namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using Internal.Routing;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Routing;

    public static class HttpContextExtensions
    {
        public static void SetRouteData(this HttpContext httpContext, RouteData routeData)
        {
            httpContext.Features[typeof(IRoutingFeature)] = new RoutingFeatureMock
            {
                RouteData = routeData
            };

            httpContext.Features[typeof(IRouteValuesFeature)] = new RouteValuesFeature
            {
                RouteValues = routeData.Values
            };
        }
    }
}
