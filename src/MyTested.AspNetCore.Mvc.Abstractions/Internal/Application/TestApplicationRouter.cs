namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Internal.Routing;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static partial class TestApplication
    {
        private static volatile IRouter router;

        public static IRouter Router
        {
            get
            {
                TryLockedInitialization();
                return router;
            }
        }

        internal static Action<IRouteBuilder> AdditionalRouting { get; set; }

        private static void PrepareApplicationAndRouting()
        {
            var applicationBuilder = new ApplicationBuilderMock(routingServiceProvider);

            startupMethods?.ConfigureDelegate?.Invoke(applicationBuilder);

            AdditionalApplicationConfiguration?.Invoke(applicationBuilder);

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
                var router = (IRouter)createAttributeMegaRouteMethod.Invoke(null, new[] { serviceProvider });

                routeBuilderRoutes.Insert(0, router);
            }

            router = routeBuilder.Build();
        }
    }
}
