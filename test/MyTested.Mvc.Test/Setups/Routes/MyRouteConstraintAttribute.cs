namespace MyTested.Mvc.Tests.Setups.Routes
{
    using Microsoft.AspNet.Mvc.Infrastructure;

    public class MyRouteConstraintAttribute : RouteConstraintAttribute
    {
        public MyRouteConstraintAttribute(string routeKey, string routeValue)
            : base(routeKey, routeValue, true)
        {
        }
    }
}
