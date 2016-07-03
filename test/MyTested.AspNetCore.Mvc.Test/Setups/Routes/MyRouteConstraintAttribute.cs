namespace MyTested.AspNetCore.Mvc.Test.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc.Routing;

    public class MyRouteConstraintAttribute : RouteValueAttribute
    {
        public MyRouteConstraintAttribute(string routeKey, string routeValue)
            : base(routeKey, routeValue)
        {
        }
    }
}
