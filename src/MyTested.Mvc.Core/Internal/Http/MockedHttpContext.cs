namespace MyTested.Mvc.Internal.Http
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Http.Internal;
    using Utilities.Validators;

    /// <summary>
    /// Mocked HTTP context object.
    /// </summary>
    public class MockedHttpContext : DefaultHttpContext
    {
        private readonly HttpResponse httpResponse;

        private HttpRequest httpRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext"/> class.
        /// </summary>
        public MockedHttpContext()
            : this(new FeatureCollection())
        {
            this.PrepareFeatures();
            this.PrepareDefaultValues();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext"/> class with the provided features.
        /// </summary>
        /// <param name="features">HTTP features to initialize.</param>
        public MockedHttpContext(IFeatureCollection features)
            : base(features)
        {
            this.CustomRequest = this.httpRequest;
            this.httpResponse = this.httpResponse ?? new MockedHttpResponse(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpContext"/> class by copying the properties from the provided one.
        /// </summary>
        /// <param name="context">HttpContext to copy properties from.</param>
        private MockedHttpContext(HttpContext context)
            : this(context.Features)
        {
            CommonValidator.CheckForNullReference(context, nameof(HttpContext));

            this.PrepareFeatures();

            this.httpRequest = context.Request;
            this.httpResponse = MockedHttpResponse.From(this, context.Response);
            this.Items = context.Items;
            this.RequestAborted = context.RequestAborted;
            this.RequestServices = context.RequestServices;
            this.TraceIdentifier = context.TraceIdentifier;
            this.User = context.User;

            if (this.Features.Get<ISessionFeature>() != null)
            {
                this.Session = context.Session;
            }

            this.PrepareDefaultValues();
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
            set { this.httpRequest = value ?? new DefaultHttpRequest(this); }
        }

        public static MockedHttpContext From(HttpContext httpContext)
        {
            return new MockedHttpContext(httpContext);
        }

        private void PrepareFeatures()
        {
            if (this.Features.Get<IHttpRequestFeature>() == null)
            {
                this.Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            }

            if (this.Features.Get<IHttpResponseFeature>() == null)
            {
                this.Features.Set<IHttpResponseFeature>(new HttpResponseFeature());
            }

            if (this.Features.Get<IServiceProvidersFeature>() == null)
            {
                this.Features.Set<IServiceProvidersFeature>(new MockedRequestServicesFeature());
            }

            TestHelper.ApplyHttpFeatures(this);
        }

        private void PrepareDefaultValues()
        {
            if (this.Request.ContentType == null)
            {
                this.Request.ContentType = ContentType.FormUrlEncoded;
            }
        }
    }
}
