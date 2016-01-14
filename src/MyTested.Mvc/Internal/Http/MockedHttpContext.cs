namespace MyTested.Mvc.Internal.Http
{
    using Identity;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Features;
    using Microsoft.AspNet.Http.Features.Internal;
    using Microsoft.AspNet.Http.Internal;
    using Microsoft.AspNet.Routing;

    public class MockedHttpContext : DefaultHttpContext
    {
        private HttpResponse httpResponse;

        public MockedHttpContext()
        {
            this.httpResponse = new MockedHttpResponse(this, this.Features);
            this.PrepareRequestServices();
        }

        public override HttpResponse Response => this.httpResponse;

        private void PrepareRequestServices()
        {
            if (!TestServiceProvider.IsAvailable)
            {
                return;
            }
            
            using (var feature = new MockedRequestServicesFeature(TestServiceProvider.Current))
            {
                this.Features.Set<IServiceProvidersFeature>(feature);
            }
        }
    }
}
