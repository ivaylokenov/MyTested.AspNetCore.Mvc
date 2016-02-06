namespace MyTested.Mvc.Builders.Routes
{
    using System;
    using Contracts.Routes;
    using Contracts.Http;
    using Microsoft.AspNet.Routing;
    using Microsoft.AspNet.Http;
    using Http;
    using Internal.Http;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public class RouteTestBuilder : BaseRouteTestBuilder, IRouteTestBuilder
    {
        private const string GetMethod = "GET";

        private MockedHttpContext httpContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTestBuilder" /> class.
        /// </summary>
        /// <param name="router">Instance of IRouter.</param>
        public RouteTestBuilder(IRouter router, IServiceProvider serviceProvider)
            : base(router, serviceProvider)
        {
            this.httpContext = new MockedHttpContext();
        }

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(string location)
        {
            return this.ShouldMap(request => request
                .WithPath(location)
                .WithMethod(GetMethod));
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
                .WithMethod(GetMethod));
        }

        /// <summary>
        /// Sets the route HTTP request message to test.
        /// </summary>
        /// <param name="request">Instance of type HttpRequest.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(HttpRequest request)
        {
            this.httpContext.CustomRequest = request;
            return this.ShouldMap(this.httpContext);
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
            httpBuilder.ApplyTo(httpContext.Request);
            return this.ShouldMap(httpContext);
        }

        private IShouldMapTestBuilder ShouldMap(HttpContext httpContext)
        {
            var routeContext = new RouteContext(httpContext);
            return new ShouldMapTestBuilder(
                this.Router,
                this.Services,
                routeContext);
        }
    }
}
