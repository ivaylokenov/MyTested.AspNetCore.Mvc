namespace MyTested.Mvc.Internal.TestContexts
{
    using System;
    using Microsoft.AspNetCore.Routing;

    public class RouteTestContext : HttpTestContext
    {
        private RouteContext routeContext;

        public IRouter Router { get; internal set; }

        public IServiceProvider Services { get; internal set; }

        public RouteContext RouteContext
        {
            get
            {
                if (this.routeContext == null)
                {
                    routeContext = new RouteContext(this.HttpContext);
                }

                return routeContext;
            }
        }

        public override string ExceptionMessagePrefix => $"Expected route '{this.HttpContext.Request.Path}'";
    }
}
