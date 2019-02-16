namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Services;
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
            var knownServicesReplaced = false;

            if (startupMethods?.ConfigureServicesDelegate != null)
            {
                var startupServiceProvider = PrepareStartupServices(serviceCollection);
                var hasTestMarkerService = ValidateTestMarkerService(startupServiceProvider);
                
                if (hasTestMarkerService)
                {
                    knownServicesReplaced = true;
                    serviceProvider = startupServiceProvider;
                }
            }
            else
            {
                PrepareDefaultServices(serviceCollection);
            }

            if (!knownServicesReplaced)
            {
                AdditionalServices?.Invoke(serviceCollection);

                TryReplaceKnownServices(serviceCollection);
            }
            
            serviceProvider = serviceProvider ?? serviceCollection.BuildServiceProviderFromFactory();

            PrepareRoutingServices();
            EnsureApplicationParts(serviceProvider);

            PluginsContainer.InitializationPlugins.ForEach(plugin => plugin.InitializationDelegate(serviceProvider));
        }

        private static bool ValidateTestMarkerService(IServiceProvider startupServiceProvider)
        {
            var testMarkerService = startupServiceProvider.GetService<TestMarkerService>();

            if (testMarkerService != null)
            {
                return true;
            }
            
            var serviceProviderReturnType = HasConfigureServicesIServiceProviderDelegate();
            if (serviceProviderReturnType)
            {
                throw new InvalidOperationException($"Testing services could not be resolved. If your {nameof(IStartup.ConfigureServices)} method returns an {nameof(IServiceProvider)}, you should either change it to return 'void' or manually register the required testing services by calling one of the provided {nameof(IServiceCollection)} extension methods in the '{TestFramework.TestFrameworkName}' namespace.");
            }

            return false;
        }

        private static void PrepareDefaultServices(IServiceCollection serviceCollection)
        {
            var defaultRegistrationPlugin = PluginsContainer.DefaultRegistrationPlugins
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
                return startupMethods.ConfigureServicesDelegate(serviceCollection);
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException is FileNotFoundException)
                {
                    throw new InvalidOperationException(
                        $"Application dependencies could not be loaded correctly. If your web project references the '{AspNetCoreMetaPackageName}' package, you need to reference it in your test project too. Additionally, make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file.");
                }
                
                throw;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Services could not be configured. If your web project is registering services outside of the Startup class (during the WebHost configuration in the Program.cs file for example), you should provide them to the test framework too by calling 'IsRunningOn(server => server.WithServices(servicesAction))'. Since this method should be called only once per test project, you may invoke it in the static constructor of your TestStartup class.");
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

        private static void PrepareRoutingServices()
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
            
            var routingServiceCollection = routingServices.ServiceCollection.Clone();

            foreach (var routingServiceRegistrationPlugin in PluginsContainer.RoutingServiceRegistrationPlugins)
            {
                routingServiceRegistrationPlugin.RoutingServiceRegistrationDelegate(routingServiceCollection);
            }

            routingServiceProvider = routingServiceCollection.BuildServiceProviderFromFactory();
        }
    }
}
