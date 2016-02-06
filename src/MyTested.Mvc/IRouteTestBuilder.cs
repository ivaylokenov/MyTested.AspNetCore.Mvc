namespace MyTested.Mvc
{
    using Builders.Contracts.Http;
    using Builders.Contracts.Routes;
    using Microsoft.AspNet.Http;
    using System;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public interface IRouteTestBuilder
    {
        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(string location);

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as Uri.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(Uri location);

        /// <summary>
        /// Sets the route HTTP request message to test.
        /// </summary>
        /// <param name="request">Instance of type HttpRequest.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(HttpRequest request);

        /// <summary>
        /// Sets the route HTTP request message to test using a builder.
        /// </summary>
        /// <param name="httpRequestBuilder">Builder for HTTP request message.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(Action<IHttpRequestBuilder> httpRequestBuilder);
    }
}
