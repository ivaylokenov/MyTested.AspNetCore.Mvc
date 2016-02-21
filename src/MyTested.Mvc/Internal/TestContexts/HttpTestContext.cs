namespace MyTested.Mvc.Internal.TestContexts
{
    using Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using MyTested.Mvc.Internal.Http;
    using Routes;
    using Utilities.Validators;
    public class HttpTestContext
    {
        private MockedHttpContext mockedHttpContext;
        private RouteData routeData;

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
                CommonValidator.CheckForNullReference(value, nameof(HttpContext));
                this.mockedHttpContext = MockedHttpContext.From(value);
            }
        }

        public HttpRequest HttpRequest => this.HttpContext.Request;

        public HttpResponse HttpResponse => this.HttpContext.Response;

        public virtual RouteData RouteData
        {
            get
            {
                if (this.routeData == null)
                {
                    this.routeData = this.HttpContext.GetRouteData();
                    if (this.routeData == null)
                    {
                        this.routeData = InternalRouteResolver.ResolveRouteData(TestApplication.Router, this.HttpContext);
                    }
                }
                
                return this.routeData;
            }

            internal set
            {
                CommonValidator.CheckForNullReference(value, nameof(RouteData));
                this.routeData = value;
            }
        }

        internal MockedHttpContext MockedHttpContext => this.mockedHttpContext;
    }
}
