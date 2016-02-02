namespace MyTested.Mvc.Internal.Application
{
    using Caching;
    using Contracts;
    using Logging;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting.Internal;
    using Microsoft.AspNet.Hosting.Startup;
    using Microsoft.AspNet.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using System;

    public static class TestApplication
    {
        private static StartupLoader startupLoader;
        private static IServiceProvider serviceProvider;
        private static IRouter router;

        static TestApplication()
        {
            startupLoader =
                new StartupLoader(new ServiceCollection().BuildServiceProvider(), new HostingEnvironment());
        }

        internal static Type StartupType { get; set; }

        internal static Action<IServiceCollection> AdditionalServices { get; set; }

        internal static Action<IApplicationBuilder> AdditionalConfiguration { get; set; }

        internal static Action<IRouteBuilder> AdditionalRoutes { get; set; }

        public static void Initialize()
        {
            var serviceCollection = GetInitialServiceCollection();

            StartupMethods startupMethods = null;
            if (StartupType != null)
            {
                startupMethods = startupLoader.LoadMethods(StartupType, null);
            }

            PrepareServices(serviceCollection, startupMethods);
            PrepareApplicationAndRoutes(startupMethods);
        }

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<ILoggerFactory>(MockedLoggerFactory.Create());
            serviceCollection.TryAddSingleton<IControllerActionDescriptorCache, ControllerActionDescriptorCache>();
            return serviceCollection;
        }

        private static void PrepareServices(IServiceCollection serviceCollection, StartupMethods startupMethods)
        {
            if (startupMethods?.ConfigureServicesDelegate != null)
            {
                startupMethods.ConfigureServicesDelegate(serviceCollection);
            }
            else
            {
                serviceCollection.AddMvc();
            }

            if (AdditionalServices != null)
            {
                AdditionalServices(serviceCollection);
            }

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void PrepareApplicationAndRoutes(StartupMethods startupMethods)
        {
            var applicationBuilder = new MockedApplicationBuilder(serviceProvider);

            if (startupMethods?.ConfigureDelegate != null)
            {
                startupMethods.ConfigureDelegate(applicationBuilder);
            }

            if (AdditionalConfiguration != null)
            {
                AdditionalConfiguration(applicationBuilder);
            }

            // use route builder and collection with null handler
            var routes = applicationBuilder.Routes;

            if (AdditionalRoutes != null)
            {
                var routeBuilder = new RouteBuilder(applicationBuilder);
                AdditionalRoutes(routeBuilder);

                for (int i = 0; i < routeBuilder.Routes.Count; i++)
                {
                    routes.Add(routeBuilder.Routes[i]);
                }
            }

            router = routes;
        }
    }
}
