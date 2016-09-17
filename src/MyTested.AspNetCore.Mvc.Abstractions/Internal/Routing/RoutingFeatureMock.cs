namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using Microsoft.AspNetCore.Routing;

    public class RoutingFeatureMock : IRoutingFeature
    {
        public RouteData RouteData { get; set; }
    }
}
