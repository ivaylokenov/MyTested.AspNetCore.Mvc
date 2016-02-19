namespace MyTested.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Mocked HTTP response.
    /// </summary>
    public class MockedHttpResponse : DefaultHttpResponse
    {
        public static MockedHttpResponse From(DefaultHttpContext httpContext, HttpResponse httpResponse)
        {
            return new MockedHttpResponse(httpContext, httpContext.Features)
            {
                Body = httpResponse.Body,
                ContentLength = httpResponse.ContentLength,
                ContentType = httpResponse.ContentType,
                StatusCode = httpResponse.StatusCode
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedHttpResponse" /> class.
        /// </summary>
        /// <param name="context">Default HTTP context.</param>
        /// <param name="features">HTTP features collection.</param>
        public MockedHttpResponse(DefaultHttpContext context, IFeatureCollection features)
            : base(context, features)
        {
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
