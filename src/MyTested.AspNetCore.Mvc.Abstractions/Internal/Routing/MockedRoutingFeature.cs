namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using Microsoft.AspNetCore.Routing;

    public class MockedRoutingFeature : IRoutingFeature
    {
        public RouteData RouteData { get; set; }
    }
}
