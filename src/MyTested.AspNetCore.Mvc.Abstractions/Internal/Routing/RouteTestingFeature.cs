namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    public class RouteTestingFeature
    {
        public RouteTestingFeature(bool fullExecution)
            => this.FullExecution = fullExecution;

        public bool FullExecution { get; private set; }
    }
}
