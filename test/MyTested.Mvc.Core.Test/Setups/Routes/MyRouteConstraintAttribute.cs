namespace MyTested.Mvc.Test.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc.Routing;

    public class MyRouteConstraintAttribute : RouteConstraintAttribute
    {
        public MyRouteConstraintAttribute(string routeKey, string routeValue)
            : base(routeKey, routeValue, false)
        {
        }
    }
}
