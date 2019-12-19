﻿namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Utilities;
    using Utilities.Extensions;

    public static class RouteDataResolver
    {
        public static RouteData ResolveRouteData(IRouter router, HttpContext httpContext) 
            => ResolveRouteData(router, new RouteContext(httpContext));

        public static RouteData ResolveRouteData(IRouter router, RouteContext routeContext)
        {
            var path = routeContext.HttpContext.Request?.Path;
            if (path == null || path.Value == string.Empty)
            {
                return null;
            }

            AsyncHelper.RunSync(() => router.RouteAsync(routeContext));

            var routeData = routeContext.RouteData;
            routeContext.HttpContext.SetRouteData(routeData);

            return routeData;
        }
    }
}
