namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Builder;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;
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

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var diagnosticListener = new DiagnosticListener(TestFrameworkName);

            // Default server services.
            serviceCollection.AddSingleton(Environment);
            serviceCollection.AddSingleton<IApplicationLifetime, ApplicationLifetime>();

            serviceCollection.AddTransient<IApplicationBuilderFactory, ApplicationBuilderFactory>();
            serviceCollection.AddTransient<IHttpContextFactory, HttpContextFactory>();
            serviceCollection.AddScoped<IMiddlewareFactory, MiddlewareFactory>();
            serviceCollection.AddOptions();

            serviceCollection.AddSingleton<ILoggerFactory>(LoggerFactoryMock.Create());
            serviceCollection.AddLogging();

            serviceCollection.AddSingleton(TestConfiguration.Configuration);

            serviceCollection.AddSingleton(diagnosticListener);
            serviceCollection.AddSingleton<DiagnosticSource>(diagnosticListener);

            serviceCollection.AddTransient<IStartupFilter, AutoRequestServicesStartupFilter>();
            serviceCollection.AddTransient<IServiceProviderFactory<IServiceCollection>, DefaultServiceProviderFactory>();

            serviceCollection.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            return serviceCollection;
        }

        private static void PrepareServices(IServiceCollection serviceCollection, StartupMethods startupMethods)
        {
            if (startupMethods?.ConfigureServicesDelegate != null)
            {
                PrepareStartupServices(serviceCollection, startupMethods);
            }
            else
            {
                PrepareDefaultServices(serviceCollection);
            }

            AdditionalServices?.Invoke(serviceCollection);

            TryReplaceKnownServices(serviceCollection);
            PrepareRoutingServices(serviceCollection);
            EnsureApplicationParts(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            InitializationPlugins.ForEach(plugin => plugin.InitializationDelegate(serviceProvider));
        }

        private static void PrepareDefaultServices(IServiceCollection serviceCollection)
        {
            var defaultRegistrationPlugin = DefaultRegistrationPlugins
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

        private static void PrepareStartupServices(IServiceCollection serviceCollection, StartupMethods startupMethods)
        {
            try
            {
                startupMethods.ConfigureServicesDelegate(serviceCollection);
            }
            catch
            {
                throw new InvalidOperationException($"Application dependencies could not be loaded correctly. If your web project references the '{AspNetCoreMetaPackageName}' package, you need to reference it in your test project too. Additionally, make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file.");
            }
        }

        private static void TryReplaceKnownServices(IServiceCollection serviceCollection)
        {
            var applicablePlugins = new HashSet<IServiceRegistrationPlugin>();

            serviceCollection.ForEach(service =>
            {
                TestServiceProvider.SaveServiceLifetime(service.ServiceType, service.Lifetime);

                foreach (var serviceRegistrationPlugin in ServiceRegistrationPlugins)
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

        private static void PrepareRoutingServices(IServiceCollection serviceCollection)
        {
            var routingServiceCollection = new ServiceCollection().Add(serviceCollection);

            foreach (var routingServiceRegistrationPlugin in RoutingServiceRegistrationPlugins)
            {
                routingServiceRegistrationPlugin.RoutingServiceRegistrationDelegate(routingServiceCollection);
            }

            routingServiceProvider = routingServiceCollection.BuildServiceProvider();
        }
    }
}
