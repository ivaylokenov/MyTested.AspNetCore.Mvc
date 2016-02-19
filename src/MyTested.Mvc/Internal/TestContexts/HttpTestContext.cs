namespace MyTested.Mvc.Internal.TestContexts
{
    using Application;
    using Microsoft.AspNetCore.Http;
    using MyTested.Mvc.Internal.Http;

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

        internal MockedHttpContext MockedHttpContext => this.mockedHttpContext;
    }
}
