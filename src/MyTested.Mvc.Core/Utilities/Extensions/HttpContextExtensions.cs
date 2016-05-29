namespace MyTested.Mvc.Utilities.Extensions
{
    using Internal.Routes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public static class HttpContextExtensions
    {
        public static void SetRouteData(this HttpContext httpContext, RouteData routeData)
        {
            httpContext.Features[typeof(IRoutingFeature)] = new MockedRoutingFeature
            {
                RouteData = routeData
            };
        }
    }
}
