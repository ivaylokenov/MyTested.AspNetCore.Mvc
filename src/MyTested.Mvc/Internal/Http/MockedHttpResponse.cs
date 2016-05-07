namespace MyTested.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    /// <summary>
    /// Mocked HTTP response.
    /// </summary>
    public class MockedHttpResponse : DefaultHttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpResponse"/> class.
        /// </summary>
        /// <param name="context">Default HTTP context.</param>
        public MockedHttpResponse(DefaultHttpContext context)
            : base(context)
        {
        }

        public static MockedHttpResponse From(DefaultHttpContext httpContext, HttpResponse httpResponse)
        {
            return new MockedHttpResponse(httpContext)
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
