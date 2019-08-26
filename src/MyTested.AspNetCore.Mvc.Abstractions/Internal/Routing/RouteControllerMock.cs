namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    internal class RouteControllerMock
    {
        // Used by the route testing services to fake the actual action call.
        public RouteActionResultMock ActionMock()
            => new RouteActionResultMock();
    }
}
