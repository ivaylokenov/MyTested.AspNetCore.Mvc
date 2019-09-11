namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Linq;
    using System.Linq.Expressions;
    using Application;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Routing;
    using Utilities.Validators;

    public abstract class HttpTestContext
    {
        private RouteData routeData;

        protected HttpTestContext()
        {
            TestHelper.ExecuteTestCleanup();
            this.HttpContextMock = TestHelper.CreateHttpContextMock();
        }

        public HttpContext HttpContext
        {
            get => this.HttpContextMock;

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.HttpContext));
                this.HttpContextMock = HttpContextMock.From(value);
                TestHelper.SetHttpContextToAccessor(this.HttpContextMock);
            }
        }

        public HttpContextMock HttpContextMock { get; private set; }

        public HttpRequest HttpRequest => this.HttpContext.Request;

        public HttpResponse HttpResponse => this.HttpContext.Response;

        public virtual RouteData RouteData
        {
            get
            {
                if (this.routeData == null)
                {
                    this.routeData = this.HttpContext.GetRouteData();
                    if (this.routeData == null || !this.routeData.Routers.Any())
                    {
                        this.routeData = RouteDataResolver.ResolveRouteData(TestApplication.Router, this.HttpContext);
                    }
                }
                
                return this.routeData;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.RouteData));
                this.routeData = value;
            }
        }

        public virtual LambdaExpression RouteDataMethodCall => null;

        public ISession Session => this.HttpContext.Session;

        public abstract string ExceptionMessagePrefix { get; }
    }
}
