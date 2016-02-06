namespace MyTested.Mvc.Tests.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc.Routing;

    public class MyRouteConstraintAttribute : RouteConstraintAttribute
    {
        public MyRouteConstraintAttribute(string routeKey, string routeValue)
            : base(routeKey, routeValue, true)
        {
        }
    }
}
