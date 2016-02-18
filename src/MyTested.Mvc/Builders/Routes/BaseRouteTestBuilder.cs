namespace MyTested.Mvc.Builders.Routes
{
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using System;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all route test builders.
    /// </summary>
    public abstract class BaseRouteTestBuilder
    {
        private RouteTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRouteTestBuilder" /> class.
        /// </summary>
        /// <param name="router">Instance of IRouter.</param>
        /// <param name="serviceProvider">Instance of IServiceProvider.</param>
        protected BaseRouteTestBuilder(RouteTestContext testContext)
        {
            this.TestContext = testContext;
        }

        protected RouteTestContext TestContext
        {
            get
            {
                return this.testContext;
            }
            set
            {
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                this.testContext = value;
            }
        }

        /// <summary>
        /// Gets the router used in the route test.
        /// </summary>
        /// <value>Instance of IRouter.</value>
        protected IRouter Router => this.TestContext.Router;

        /// <summary>
        /// Gets the services used in the route test.
        /// </summary>
        /// <value>Instance of IServiceProvider.</value>
        protected IServiceProvider Services => this.TestContext.Services;
    }
}
