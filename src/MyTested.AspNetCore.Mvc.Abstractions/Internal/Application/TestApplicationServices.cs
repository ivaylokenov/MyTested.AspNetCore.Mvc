namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using Contracts;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Server;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        private static volatile IServiceProvider serviceProvider;
        private static volatile IServiceProvider routingServiceProvider;

        public static IServiceProvider Services
        {
            get
            {
                TryLockedInitialization();
                return serviceProvider;
            }
        }

        public static IServiceProvider RoutingServices
        {
            get
            {
                TryLockedInitialization();
                return routingServiceProvider;
            }
        }

        internal static Action<IServiceCollection> AdditionalServices { get; set; }

        private static void PrepareServices(IServiceCollection serviceCollection)
        {
            try
            {
                var knownServicesReplaced = false;

                if (startupMethods?.ConfigureServicesDelegate != null)
                {
                    var startupServiceProvider = PrepareStartupServices(serviceCollection);
                    var hasTestMarkerService = ValidateTestServices(startupServiceProvider);

                    if (hasTestMarkerService)
                    {
                        knownServicesReplaced = true;
                        serviceProvider = startupServiceProvider;
                    }
                }
                else
                {
                    // Server additional services delegate is never invoked because Startup is null.
                    TestWebServer.AdditionalServices?.Invoke(serviceCollection);

                    PrepareDefaultServices(serviceCollection);
                }

                if (!knownServicesReplaced)
                {
                    AdditionalServices?.Invoke(serviceCollection);

                    TryReplaceKnownServices(serviceCollection);
                }

                serviceProvider ??= serviceCollection.BuildServiceProviderFromFactory();

                PrepareRoutingServices(knownServicesReplaced);
                EnsureApplicationParts(serviceProvider);

                PluginsContainer.InitializationPlugins.ForEach(plugin => plugin.InitializationDelegate(serviceProvider));
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException)
                {
                    throw;
                }

                throw new InvalidOperationException($"Test application could not be initialized. You may need to create a custom mock for one of your registered services. If you are having difficulties debugging this error, open an issue at https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/issues. Provide your Startup classes and this exception message: '{exception.Message}'.");
            }
        }

        private static bool ValidateTestServices(IServiceProvider startupServiceProvider)
        {
            var testMarkerService = startupServiceProvider.GetService<TestMarkerService>();
            if (testMarkerService != null)
            {
                return true;
            }

            ValidateCustomServiceProvider();

            return false;
        }

        private static void PrepareDefaultServices(IServiceCollection serviceCollection)
        {
            var defaultRegistrationPlugin = PluginsContainer
                .DefaultRegistrationPlugins
                .OrderByDescending(p => p.Priority)
                .FirstOrDefault();

            if (defaultRegistrationPlugin != null)
            {
                defaultRegistrationPlugin.DefaultServiceRegistrationDelegate(serviceCollection);
            }
            else
            {
                serviceCollection.AddMvcCore();
            }
        }

        private static IServiceProvider PrepareStartupServices(IServiceCollection serviceCollection)
        {
            try
            {
                return startupMethods.ConfigureServicesDelegate?.Invoke(serviceCollection);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException($"An exception with the following message was thrown during initialization: '{exception.Message}'. Services could not be configured. If your web project is registering services outside of the Startup class (during the WebHost configuration in the Program.cs file for example), you should provide them to the test framework too by calling 'IsRunningOn(server => server.WithServices(servicesAction))'. Since this method should be called only once per test project, you may invoke it in the static constructor of your {TestWebServer.Environment.EnvironmentName}Startup class or if your test runner supports it - in the test assembly initialization.");
            }
        }

        private static void TryReplaceKnownServices(IServiceCollection serviceCollection)
        {
            var applicablePlugins = new HashSet<IServiceRegistrationPlugin>();

            serviceCollection.ForEach(service =>
            {
                TestServiceProvider.SaveServiceLifetime(service.ServiceType, service.Lifetime);

                foreach (var serviceRegistrationPlugin in PluginsContainer.ServiceRegistrationPlugins)
                {
                    if (serviceRegistrationPlugin.ServiceSelectorPredicate(service))
                    {
                        applicablePlugins.Add(serviceRegistrationPlugin);
                    }
                }
            });

            foreach (var applicablePlugin in applicablePlugins)
            {
                applicablePlugin.ServiceRegistrationDelegate(serviceCollection);
            }
        }

        private static void PrepareRoutingServices(bool routingServicesReplaced)
        {
            var routingServices = serviceProvider.GetRequiredService<IRoutingServices>();
            if (routingServices.ServiceProvider != null)
            {
                routingServiceProvider = routingServices.ServiceProvider;
                return;
            }

            if (routingServices.ServiceCollection == null)
            {
                throw new InvalidOperationException($"Route testing requires the registered {nameof(IRoutingServices)} service implementation to provide test routing services by either the {nameof(IRoutingServices.ServiceProvider)} property or the {nameof(IRoutingServices.ServiceCollection)} one.");
            }

            if (!routingServicesReplaced)
            {
                foreach (var routingServiceRegistrationPlugin in PluginsContainer.RoutingServiceRegistrationPlugins)
                {
                    routingServiceRegistrationPlugin
                        .RoutingServiceRegistrationDelegate(routingServices.ServiceCollection);
                }
            }

            var configureContainerMethod = GetConfigureContainerMethod();
            if (configureContainerMethod == null)
            {
                routingServiceProvider = routingServices.ServiceCollection.BuildServiceProviderFromFactory();
            }
            else
            {
                var containerType = configureContainerMethod
                    .GetParameters()
                    .FirstOrDefault()
                    ?.ParameterType;

                if (containerType != null)
                {
                    var routingServiceProviderBuilder = (RoutingServiceProviderBuilder)Activator.CreateInstance(
                        typeof(RoutingServiceProviderBuilder<>).MakeGenericType(containerType),
                        serviceProvider,
                        routingServices.ServiceCollection,
                        startupMethods,
                        configureContainerMethod);

                    routingServiceProvider = routingServiceProviderBuilder.BuildServiceProvider();
                }
            }
        }

        private static void ValidateCustomServiceProvider()
        {
            if (StartupType == null)
            {
                return;
            }

            var hasConfigureServicesIServiceProviderDelegate = null != FindMethodDelegate.Invoke(null, new object[]
            {
                StartupType,
                "Configure{0}Services",
                TestWebServer.Environment.EnvironmentName,
                typeof(IServiceProvider),
                false
            });

            if (hasConfigureServicesIServiceProviderDelegate)
            {
                var additionalExceptionMessageForStartupType = string.Empty;
                if (StartupType.Name != $"{TestWebServer.Environment.EnvironmentName}Startup")
                {
                    additionalExceptionMessageForStartupType = $" An easy way to do the second option is to add a {TestWebServer.Environment.EnvironmentName}Startup class at the root of your test project and invoke the extension methods there.";
                }

                throw new InvalidOperationException($"Testing services could not be resolved. If your {nameof(IStartup.ConfigureServices)} method returns an {nameof(IServiceProvider)}, you should either change it to return 'void' or manually register the required testing services by calling one of the provided {nameof(IServiceCollection)} extension methods in the '{TestFramework.TestFrameworkName}' namespace.{additionalExceptionMessageForStartupType}");
            }
        }

        // Adapted from the ASP.NET Core source code.
        private abstract class RoutingServiceProviderBuilder
        {
            public abstract IServiceProvider BuildServiceProvider();
        }

        private class RoutingServiceProviderBuilder<TContainerBuilder> : RoutingServiceProviderBuilder
            where TContainerBuilder : class
        {
            private readonly IServiceProvider serverServiceProvider;
            private readonly IServiceCollection services;
            private readonly dynamic startupTypeMethods;
            private readonly MethodInfo configureContainerMethod;

            public RoutingServiceProviderBuilder(
                IServiceProvider serverServiceProvider,
                IServiceCollection services,
                dynamic startupTypeMethods,
                MethodInfo configureContainerMethod)
            {
                this.serverServiceProvider = serverServiceProvider;
                this.services = services;
                this.startupTypeMethods = startupTypeMethods;
                this.configureContainerMethod = configureContainerMethod;
            }

            public override IServiceProvider BuildServiceProvider()
            {
                var configureContainerBuilder = Activator
                    .CreateInstance(
                        WebFramework.Internals.ConfigureContainerBuilder,
                        this.configureContainerMethod)
                    .Exposed();

                Func<Action<object>, Action<object>> configureContainerPipeline = this.ConfigureContainerPipeline;

                configureContainerBuilder.ConfigureContainerFilters = configureContainerPipeline;

                var serviceProviderFactory = serviceProvider.GetRequiredService<IServiceProviderFactory<TContainerBuilder>>();
                var builder = serviceProviderFactory.CreateBuilder(this.services);

                configureContainerBuilder
                    .Build(this.startupTypeMethods.StartupInstance)
                    .Invoke(builder);

                return serviceProviderFactory.CreateServiceProvider(builder);
            }

            private Action<object> ConfigureContainerPipeline(Action<object> action)
                => containerBuilder => this.BuildFiltersPipeline(action)((TContainerBuilder)containerBuilder);

            private Action<TContainerBuilder> BuildFiltersPipeline(
                Action<TContainerBuilder> configureContainer)
                => builder => this.serverServiceProvider
#pragma warning disable CS0612 // Type or member is obsolete
                    .GetRequiredService<IEnumerable<IStartupConfigureContainerFilter<TContainerBuilder>>>()
#pragma warning restore CS0612 // Type or member is obsolete
                    .Reverse()
                    .Aggregate(configureContainer, (current, filter) => filter
                        .ConfigureContainer(current))(builder);
        }
    }
}