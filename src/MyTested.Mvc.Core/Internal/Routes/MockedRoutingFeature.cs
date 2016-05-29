namespace MyTested.Mvc.Internal.Routes
{
    using Microsoft.AspNetCore.Routing;

    public class MockedRoutingFeature : IRoutingFeature
    {
        public RouteData RouteData { get; set; }
    }
}
