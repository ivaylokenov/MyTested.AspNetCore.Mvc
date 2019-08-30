namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Http.Features.Authentication;

    public class RouteTestContext : HttpTestContext
    {
        private RouteContext routeContext;

        public RouteTestContext()
        {
            this.SetAuthentication();
        }

        public IRouter Router { get; internal set; }

        public IServiceProvider Services { get; internal set; }

        public RouteContext RouteContext
        {
            get
            {
                if (this.routeContext == null)
                {
                    this.routeContext = new RouteContext(this.HttpContext);
                }

                return this.routeContext;
            }
        }

        public override string ExceptionMessagePrefix => $"Expected route '{this.HttpContext.Request.Path}'";

        private void SetAuthentication()
        {
            var httpAuthenticationFeature =
                this.HttpContext.Features.Get<IHttpAuthenticationFeature>()
                ?? new HttpAuthenticationFeature();

            this.HttpContext.Features.Set(httpAuthenticationFeature);
        }
    }
}
