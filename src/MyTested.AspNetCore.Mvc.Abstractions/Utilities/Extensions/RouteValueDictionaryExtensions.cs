namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using Microsoft.AspNetCore.Routing;

    public static class RouteValueDictionaryExtensions
    {
        public static void Add(this RouteValueDictionary routeValueDictionary, object routeValues)
        {
            if (routeValues != null)
            {
                var additionalRouteValues = new RouteValueDictionary(routeValues);

                foreach (var additionalRouteValue in additionalRouteValues)
                {
                    routeValueDictionary[additionalRouteValue.Key] = additionalRouteValue.Value;
                }
            }
        }
    }
}
