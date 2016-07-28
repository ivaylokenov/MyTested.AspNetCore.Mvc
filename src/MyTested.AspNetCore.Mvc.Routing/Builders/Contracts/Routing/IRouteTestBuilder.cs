namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Routing
{
    using System;
    using Http;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public interface IRouteTestBuilder
    {
        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Test builder of <see cref="IShouldMapTestBuilder"/> type.</returns>
        IShouldMapTestBuilder ShouldMap(string location);

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as <see cref="Uri"/>.</param>
        /// <returns>Test builder of <see cref="IShouldMapTestBuilder"/> type.</returns>
        IShouldMapTestBuilder ShouldMap(Uri location);

        /// <summary>
        /// Sets the <see cref="HttpRequest"/> for the route test.
        /// </summary>
        /// <param name="request">Instance of <see cref="HttpRequest"/> type.</param>
        /// <returns>Test builder of <see cref="IShouldMapTestBuilder"/> type.</returns>
        IShouldMapTestBuilder ShouldMap(HttpRequest request);

        /// <summary>
        /// Sets the <see cref="HttpRequest"/> for the route test.
        /// </summary>
        /// <param name="httpRequestBuilder">Action setting the <see cref="HttpRequest"/> by using <see cref="IHttpRequestBuilder"/>.</param>
        /// <returns>Test builder of <see cref="IShouldMapTestBuilder"/> type.</returns>
        IShouldMapTestBuilder ShouldMap(Action<IHttpRequestBuilder> httpRequestBuilder);
    }
}
