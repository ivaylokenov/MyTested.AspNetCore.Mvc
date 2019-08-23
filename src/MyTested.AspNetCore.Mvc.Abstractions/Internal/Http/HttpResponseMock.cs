namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Mock of HTTP response.
    /// </summary>
    public class HttpResponseMock : HttpResponse
    {
        private readonly HttpResponse httpResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMock"/> class.
        /// </summary>
        /// <param name="context">Default HTTP context.</param>
        public HttpResponseMock(HttpContext context) 
            => this.httpResponse = (HttpResponse)Activator.CreateInstance(WebFramework.Internals.DefaultHttpResponse, context);

        public override Stream Body 
        { 
            get => this.httpResponse.Body; 
            set => this.httpResponse.Body = value; 
        }
        
        public override long? ContentLength
        {
            get => this.httpResponse.ContentLength;
            set => this.httpResponse.ContentLength = value;
        }

        public override string ContentType
        {
            get => this.httpResponse.ContentType;
            set => this.httpResponse.ContentType = value;
        }

        public override IResponseCookies Cookies => this.httpResponse.Cookies;

        public override bool HasStarted => this.httpResponse.HasStarted;

        public override IHeaderDictionary Headers => this.httpResponse.Headers;

        public override HttpContext HttpContext => this.httpResponse.HttpContext;

        public override int StatusCode
        {
            get => this.httpResponse.StatusCode;
            set => this.httpResponse.StatusCode = value;
        }

        public static HttpResponseMock From(HttpContext httpContext, HttpResponse httpResponse)
        {
            return new HttpResponseMock(httpContext)
            {
                Body = httpResponse.Body,
                ContentLength = httpResponse.ContentLength,
                ContentType = httpResponse.ContentType,
                StatusCode = httpResponse.StatusCode
            };
        }

        public override void OnCompleted(Func<object, Task> callback, object state)
            => this.httpResponse.OnCompleted(callback, state);

        public override void OnStarting(Func<object, Task> callback, object state)
            => this.httpResponse.OnStarting(callback, state);

        public override void Redirect(string location, bool permanent)
            => this.httpResponse.Redirect(location, permanent);

        /// <summary>
        /// Does nothing. Intentionally left empty, otherwise some HTTP features are not working correctly.
        /// </summary>
        /// <param name="disposable">Disposable object.</param>
        public override void RegisterForDispose(IDisposable disposable)
        {
            // Intentionally left empty, otherwise some HTTP features are not working.
        }
    }
}
