namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    /// <summary>
    /// Mock of HTTP response.
    /// </summary>
    public class HttpResponseMock : DefaultHttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMock"/> class.
        /// </summary>
        /// <param name="context">Default HTTP context.</param>
        public HttpResponseMock(HttpContext context)
            : base(context)
        {
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
