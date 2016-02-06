namespace MyTested.Mvc.Builders.Routes
{
    using Microsoft.AspNet.Routing;
    using System;
    /// <summary>
    /// Base class for all route test builders.
    /// </summary>
    public abstract class BaseRouteTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRouteTestBuilder" /> class.
        /// </summary>
        /// <param name="router">Instance of IRouter.</param>
        /// <param name="serviceProvider">Instance of IServiceProvider.</param>
        protected BaseRouteTestBuilder(IRouter router, IServiceProvider serviceProvider)
        {
            this.Router = router;
            this.Services = serviceProvider;
        }

        /// <summary>
        /// Gets the router used in the route test.
        /// </summary>
        /// <value>Instance of IRouter.</value>
        protected IRouter Router { get; private set; }

        /// <summary>
        /// Gets the services used in the route test.
        /// </summary>
        /// <value>Instance of IServiceProvider.</value>
        protected IServiceProvider Services { get; private set; }
    }
}
