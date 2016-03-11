namespace MyTested.Mvc.Utilities.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Internal.Routes;

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
