namespace MyTested.Mvc.Internal.TestContexts
{
    using Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using MyTested.Mvc.Internal.Http;
    using Routes;
    public class HttpTestContext
    {
        private MockedHttpContext mockedHttpContext;

        public HttpTestContext()
        {
            this.mockedHttpContext = TestServiceProvider.CreateMockedHttpContext();
        }

        public HttpContext HttpContext
        {
            get
            {
                return this.mockedHttpContext;
            }

            internal set
            {
                this.mockedHttpContext = MockedHttpContext.From(value);
            }
        }

        public HttpRequest HttpRequest => this.HttpContext.Request;

        public HttpResponse HttpResponse => this.HttpContext.Response;

        public virtual RouteData RouteData
        {
            get
            {
                var routeData = this.HttpContext.GetRouteData();
                if (routeData == null)
                {
                    routeData = InternalRouteResolver.ResolveRouteData(TestApplication.Router, this.HttpContext);
                }

                return routeData;
            }
        }

        internal MockedHttpContext MockedHttpContext => this.mockedHttpContext;
    }
}
