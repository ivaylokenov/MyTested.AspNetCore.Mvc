namespace MyTested.Mvc.Internal.TestContexts
{
    using Microsoft.AspNetCore.Http;
    using MyTested.Mvc.Internal.Http;

    public class HttpTestContext
    {
        private MockedHttpContext mockedHttpContext;

        public HttpTestContext()
        {
            this.mockedHttpContext = new MockedHttpContext();
        }

        public HttpContext HttpContext
        {
            get
            {
                return this.mockedHttpContext;
            }

            internal set
            {
                this.mockedHttpContext = new MockedHttpContext(value);
            }
        }

        public HttpRequest HttpRequest => this.HttpContext.Request;

        public HttpResponse HttpResponse => this.HttpContext.Response;

        internal MockedHttpContext MockedHttpContext => this.mockedHttpContext;
    }
}
