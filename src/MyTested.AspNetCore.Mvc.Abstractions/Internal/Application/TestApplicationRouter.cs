namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Http;
    using Routing;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        private static volatile IRouter router;
        private static volatile RequestDelegate pipeline;

        public static IRouter Router
        {
            get
            {
                TryLockedInitialization();
                return router;
            }
        }

        public static RequestDelegate Pipeline
        {
            get
            {
                TryLockedInitialization();
                return pipeline;
            }
        }

        internal static Action<IRouteBuilder> AdditionalRouting { get; set; }

        private static void PrepareApplicationAndRouting()
        {
            var applicationBuilder = new ApplicationBuilderMock(routingServiceProvider);

            var configureDelegate = startupMethods?.ConfigureDelegate;

            if (configureDelegate != null)
            {
                routingServiceProvider
                    .GetService<IEnumerable<IStartupFilter>>()
                    .Reverse()
                    .ForEach(startupFilter => 
                        configureDelegate = startupFilter.Configure(configureDelegate));
            }

            configureDelegate?.Invoke(applicationBuilder);

            AdditionalApplicationConfiguration?.Invoke(applicationBuilder);

            PreparePipelineAndFeatures(applicationBuilder);
            PrepareRouter(applicationBuilder);
        }

        private static void PreparePipelineAndFeatures(IApplicationBuilder applicationBuilder)
        {
            pipeline = applicationBuilder.Build();

            var httpContext = new DefaultHttpContext();

            var featuresToRemove = new FeatureCollection(httpContext.Features);

            pipeline(httpContext);

            var defaultFeatures = new FeatureCollection();

            httpContext.Features.ForEach(feature =>
            {
                var (type, value) = feature;
                if (featuresToRemove[type] == null)
                {
                    defaultFeatures[type] = value;
                }
            });

            TestHelper.DefaultHttpFeatures = defaultFeatures;
        }

        private static void PrepareRouter(ApplicationBuilderMock applicationBuilder)
        {
            applicationBuilder.ExtractRouteConfiguration();

            var routeBuilder = new RouteBuilder(applicationBuilder)
            {
                DefaultHandler = RouteHandlerMock.Null
            };

            for (int i = 0; i < applicationBuilder.Routes.Count; i++)
            {
                var route = applicationBuilder.Routes[i];
                routeBuilder.Routes.Add(route);
            }

            AdditionalRouting?.Invoke(routeBuilder);

            var routeBuilderRoutes = routeBuilder.Routes;

            if (StartupType == null || routeBuilderRoutes.Count == 0)
            {
                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            }

            var attributeRoutingType = WebFramework.Internals.AttributeRouting;

            if (!routeBuilderRoutes[0].GetType().Name.StartsWith(nameof(Attribute)))
            {
                var createAttributeMegaRouteMethod = attributeRoutingType.GetMethod("CreateAttributeMegaRoute");
                var attributeRouter = (IRouter)createAttributeMegaRouteMethod?.Invoke(null, new object[] { serviceProvider });

                routeBuilderRoutes.Insert(0, attributeRouter);
            }

            router = routeBuilder.Build();
        }
    }
}
