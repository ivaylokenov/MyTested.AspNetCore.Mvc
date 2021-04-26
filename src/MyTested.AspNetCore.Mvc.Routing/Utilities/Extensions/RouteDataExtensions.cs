namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using Microsoft.AspNetCore.Routing;

    public static class RouteDataExtensions
    {
        public static void AddFrom(this RouteData originalRouteData, RouteData routeData)
        {
            if (originalRouteData == null || routeData == null)
            {
                return;
            }

            routeData.Values.ForEach(routeValue =>
            {
                var routeValueKey = routeValue.Key;
                if (!originalRouteData.Values.ContainsKey(routeValueKey))
                {
                    originalRouteData.Values[routeValueKey] = routeValue.Value;
                }
            });
        }
    }
}
