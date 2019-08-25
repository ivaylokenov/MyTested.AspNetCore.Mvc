namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;
    using System.Threading;
    using Utilities.Validators;

    /// <summary>
    /// Mock of HTTP context.
    /// </summary>
    public class HttpContextMock : HttpContext
    {
        private readonly DefaultHttpContext httpContext; 

        private HttpRequest httpRequest;
        private HttpResponse httpResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextMock"/> class.
        /// </summary>
        public HttpContextMock()
            : this(new FeatureCollection())
        {
            this.PrepareFeatures();
            this.PrepareDefaultValues();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextMock"/> class with the provided features.
        /// </summary>
        /// <param name="features">HTTP features to initialize.</param>
        public HttpContextMock(IFeatureCollection features)
        {
            this.httpContext = new DefaultHttpContext(features);

            this.CustomRequest = this.httpRequest;
            this.httpResponse = this.httpResponse ?? new HttpResponseMock(this.httpContext);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextMock"/> class by copying the properties from the provided one.
        /// </summary>
        /// <param name="context">HttpContext to copy properties from.</param>
        private HttpContextMock(HttpContext context)
            : this(context.Features)
        {
            CommonValidator.CheckForNullReference(context, nameof(HttpContext));

            this.PrepareFeatures();
            this.PrepareData(context);
            this.PrepareDefaultValues();
        }

        public HttpRequest CustomRequest
        {
            set => this.httpRequest = value ?? (HttpRequest)Activator.CreateInstance(
                WebFramework.Internals.DefaultHttpRequest, 
                this.httpContext);
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

        public override ConnectionInfo Connection => this.httpContext.Connection;

        public override IFeatureCollection Features => this.httpContext.Features;

        public override IDictionary<object, object> Items
        { 
            get => this.httpContext.Items; 
            set => this.httpContext.Items = value; 
        }
        
        public override CancellationToken RequestAborted
        {
            get => this.httpContext.RequestAborted;
            set => this.httpContext.RequestAborted = value;
        }

        public override IServiceProvider RequestServices
        {
            get => this.httpContext.RequestServices;
            set => this.httpContext.RequestServices = value;
        }

        public override ISession Session
        {
            get => this.httpContext.Session;
            set => this.httpContext.Session = value;
        }

        public override string TraceIdentifier
        {
            get => this.httpContext.TraceIdentifier;
            set => this.httpContext.TraceIdentifier = value;
        }

        public override ClaimsPrincipal User
        {
            get => this.httpContext.User;
            set => this.httpContext.User = value;
        }

        public override WebSocketManager WebSockets => this.httpContext.WebSockets;

        public static HttpContextMock From(HttpContext httpContext)
            => new HttpContextMock(httpContext);

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

            if (this.Features.Get<IHttpResponseBodyFeature>() == null)
            {
                this.Features.Set<IHttpResponseBodyFeature>(new StreamResponseBodyFeature(Stream.Null));
            }

            if (this.Features.Get<IServiceProvidersFeature>() == null)
            {
                this.Features.Set<IServiceProvidersFeature>(new RequestServicesFeatureMock());
            }

            TestHelper.ApplyHttpFeatures(this);
        }

        private void PrepareData(HttpContext context)
        {
            this.httpRequest = context.Request;
            this.httpResponse = HttpResponseMock.From(this, context.Response);

            this.Items = context.Items;
            this.RequestAborted = context.RequestAborted;
            this.RequestServices = context.RequestServices;
            this.TraceIdentifier = context.TraceIdentifier;
            this.User = context.User;

            if (this.Features.Get<ISessionFeature>() != null)
            {
                this.Session = context.Session;
            }
        }

        private void PrepareDefaultValues()
        {
            if (this.Request.ContentType == null)
            {
                this.Request.ContentType = ContentType.FormUrlEncoded;
            }
        }

        public override void Abort() => this.httpContext.Abort();
    }
}
