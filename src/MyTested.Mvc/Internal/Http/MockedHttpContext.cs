namespace MyTested.Mvc.Internal.Http
{
    using Internal.Application;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Features;
    using Microsoft.AspNet.Http.Features.Internal;
    using Microsoft.AspNet.Http.Internal;

    /// <summary>
    /// Mocked HTTP context object.
    /// </summary>
    public class MockedHttpContext : DefaultHttpContext
    {
        private HttpRequest httpRequest;
        private HttpResponse httpResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext" /> class.
        /// </summary>
        public MockedHttpContext()
            : this(new FeatureCollection())
        {
            this.Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            this.Features.Set<IHttpResponseFeature>(new HttpResponseFeature());
            this.Request.ContentType = ContentType.FormUrlEncoded;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext" /> class by copying the properties from the provided one.
        /// </summary>
        /// <param name="context">HttpContext to copy properties from.</param>
        public MockedHttpContext(HttpContext context)
            : this(context.Features)
        {
            this.httpRequest = context.Request;
            this.httpResponse = context.Response;
            this.Items = context.Items;
            this.RequestAborted = context.RequestAborted;
            this.RequestServices = context.RequestServices;
            // this.Session = context.Session;
            this.TraceIdentifier = context.TraceIdentifier;
            this.User = context.User;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext" /> class with the provided features.
        /// </summary>
        /// <param name="features">HTTP features to initialize.</param>
        public MockedHttpContext(IFeatureCollection features)
            : base(features)
        {
            this.CustomRequest = this.httpRequest;
            this.httpResponse = this.httpResponse ?? new MockedHttpResponse(this, this.Features);
            this.PrepareRequestServices();
        }

        /// <summary>
        /// Gets the HTTP request object.
        /// </summary>
        /// <value>Object of HttpRequest type.</value>
        public override HttpRequest Request => this.httpRequest;

        /// <summary>
        /// Gets the HTTP response object.
        /// </summary>
        /// <value>Object of HttpResponse type.</value>
        public override HttpResponse Response => this.httpResponse;

        internal HttpRequest CustomRequest
        {
            set { this.httpRequest = value ?? new DefaultHttpRequest(this, this.Features); }
        }

        private void PrepareRequestServices()
        {
            if (!TestServiceProvider.IsAvailable || this.RequestServices != null)
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
