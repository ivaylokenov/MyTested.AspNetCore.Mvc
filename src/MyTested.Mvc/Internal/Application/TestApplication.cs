namespace MyTested.Mvc.Internal.Application
{
    using System;
    using System.Threading.Tasks;
    using Caching;
    using Contracts;
    using Logging;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting.Internal;
    using Microsoft.AspNet.Hosting.Startup;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc.Routing;
    using Microsoft.AspNet.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;

    public static class TestApplication
    {
        private static readonly RequestDelegate NullHandler = (c) => Task.FromResult(0);

        private static bool initialiazed;
        private static Type startupType;
        private static StartupLoader startupLoader;

        private static IServiceProvider serviceProvider;
        private static IRouter router;

        static TestApplication()
        {
            startupLoader = new StartupLoader(
                new ServiceCollection().BuildServiceProvider(),
                new HostingEnvironment
                {
                    EnvironmentName = "Tests"
                });
        }

        internal static Type StartupType
        {
            get
            {
                return startupType;
            }
            set
            {
                Reset();
                startupType = value;
            }
        }

        internal static Action<IServiceCollection> AdditionalServices { get; set; }

        internal static Action<IApplicationBuilder> AdditionalConfiguration { get; set; }

        internal static Action<IRouteBuilder> AdditionalRoutes { get; set; }

        public static IServiceProvider Services
        {
            get
            {
                if (!initialiazed)
                {
                    Initialize();
                }

                return serviceProvider;
            }
        }

        public static IRouter Router
        {
            get
            {
                if (!initialiazed)
                {
                    Initialize();
                }

                return router;
            }
        }

        private static void Initialize()
        {
            var serviceCollection = GetInitialServiceCollection();

            StartupMethods startupMethods = null;
            if (StartupType != null)
            {
                startupMethods = startupLoader.LoadMethods(StartupType, null);
            }

            PrepareServices(serviceCollection, startupMethods);
            PrepareApplicationAndRoutes(startupMethods);

            initialiazed = true;
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

            var routeBuilder = new RouteBuilder(applicationBuilder);
            routeBuilder.DefaultHandler = new RouteHandler(NullHandler);

            for (int i = 0; i < applicationBuilder.Routes.Count; i++)
            {
                var route = applicationBuilder.Routes[i];
                routeBuilder.Routes.Add(route);
            }

            if (AdditionalRoutes != null)
            {
                AdditionalRoutes(routeBuilder);
            }

            if (StartupType == null || routeBuilder.Routes.Count == 0)
            {
                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routeBuilder.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(
                    routeBuilder.DefaultHandler,
                    serviceProvider));
            }

            router = routeBuilder.Build();
        }

        private static void Reset()
        {
            initialiazed = false;
            startupType = null;
            serviceProvider = null;
            router = null;
            AdditionalServices = null;
            AdditionalConfiguration = null;
            AdditionalRoutes = null;
        }
    }
}
