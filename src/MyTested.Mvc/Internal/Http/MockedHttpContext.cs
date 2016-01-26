namespace MyTested.Mvc.Internal.Http
{
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Features;
    using Microsoft.AspNet.Http.Features.Internal;
    using Microsoft.AspNet.Http.Internal;

    /// <summary>
    /// Mocked HTTP context object.
    /// </summary>
    public class MockedHttpContext : DefaultHttpContext
    {
        private HttpResponse httpResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext" /> class.
        /// </summary>
        public MockedHttpContext()
        {
            this.httpResponse = new MockedHttpResponse(this, this.Features);
            PrepareRequestServices(this);
        }

        /// <summary>
        /// Gets the HTTP response object.
        /// </summary>
        /// <value>Object of HttpResponse type.</value>
        public override HttpResponse Response => this.httpResponse;

        internal static void PrepareRequestServices(HttpContext httpContext)
        {
            if (!TestServiceProvider.IsAvailable)
            {
                return;
            }
            
            using (var feature = new MockedRequestServicesFeature(TestServiceProvider.Current))
            {
                httpContext.Features.Set<IServiceProvidersFeature>(feature);
            }
        }
    }
}
