namespace MyTested.Mvc.Builders.Routes
{
    using System;
    using Contracts.Routes;
    using Contracts.Http;
    using Microsoft.AspNetCore.Http;
    using Http;
    using Internal.Http;
    using Internal.TestContexts;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public class RouteTestBuilder : BaseRouteTestBuilder, IRouteTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTestBuilder" /> class.
        /// </summary>
        /// <param name="router">Instance of IRouter.</param>
        public RouteTestBuilder(RouteTestContext testContext)
            : base(testContext)
        {
        }

        private MockedHttpContext HttpContext => this.TestContext.MockedHttpContext;

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(string location)
        {
            return this.ShouldMap(request => request
                .WithLocation(location)
                .WithMethod(HttpMethod.Get));
        }

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as Uri.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(Uri location)
        {
            return this.ShouldMap(request => request
                .WithLocation(location)
                .WithMethod(HttpMethod.Get));
        }

        /// <summary>
        /// Sets the route HTTP request message to test.
        /// </summary>
        /// <param name="request">Instance of type HttpRequest.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(HttpRequest request)
        {
            this.HttpContext.CustomRequest = request;
            return this.ShouldMap(this.HttpContext);
        }

        /// <summary>
        /// Sets the route HTTP request message to test using a builder.
        /// </summary>
        /// <param name="httpRequestBuilder">Builder for HTTP request.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(Action<IHttpRequestBuilder> httpRequestBuilder)
        {
            var httpBuilder = new HttpRequestBuilder();
            httpRequestBuilder(httpBuilder);
            httpBuilder.ApplyTo(this.HttpContext.Request);
            return this.ShouldMap(this.HttpContext);
        }

        private IShouldMapTestBuilder ShouldMap(HttpContext httpContext)
        {
            return new ShouldMapTestBuilder(this.TestContext);
        }
    }
}
