namespace MyTested.Mvc.Internal.Http
{
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Internal;

    public class MockedHttpContext : DefaultHttpContext
    {
        private HttpResponse httpResponse;

        public MockedHttpContext()
        {
            this.httpResponse = new MockedHttpResponse(this, this.Features);
        }

        public override HttpResponse Response => this.httpResponse;
    }
}
