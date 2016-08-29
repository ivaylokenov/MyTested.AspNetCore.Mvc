namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Application;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Routing;
    using Utilities.Validators;

    public abstract class HttpTestContext
    {
        private HttpContextMock httpContextMock;
        private RouteData routeData;

        protected HttpTestContext()
        {
            TestHelper.ExecuteTestCleanup();
            this.httpContextMock = TestHelper.CreateHttpContextMock();
        }

        public HttpContext HttpContext
        {
            get
            {
                return this.httpContextMock;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(HttpContext));
                this.httpContextMock = HttpContextMock.From(value);
                TestHelper.SetHttpContextToAccessor(this.httpContextMock);
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
                        this.routeData = RouteDataResolver.ResolveRouteData(TestApplication.Router, this.HttpContext);
                    }
                }
                
                return this.routeData;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(RouteData));
                this.routeData = value;
            }
        }
        
        public ISession Session => this.HttpContext.Session;

        public abstract string ExceptionMessagePrefix { get; }

        public HttpContextMock HttpContextMock => this.httpContextMock;
    }
}
