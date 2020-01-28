namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Routing;

    public static partial class TestApplication
    {
        private static readonly RequestDelegate NullHandler;

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
                DefaultHandler = new RouteHandler(NullHandler)
            };

            for (int i = 0; i < applicationBuilder.Routes.Count; i++)
            {
                var route = applicationBuilder.Routes[i];
                routeBuilder.Routes.Add(route);
            }
            
            AdditionalRouting?.Invoke(routeBuilder);

            if (StartupType == null || routeBuilder.Routes.Count == 0)
            {
                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routeBuilder.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(serviceProvider));
            }

            router = routeBuilder.Build();
        }
    }
}
