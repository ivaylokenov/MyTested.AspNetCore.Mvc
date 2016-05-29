namespace MyTested.Mvc.Builders.Routes
{
    using System;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all route test builders.
    /// </summary>
    public abstract class BaseRouteTestBuilder
    {
        private RouteTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRouteTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="RouteTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseRouteTestBuilder(RouteTestContext testContext)
        {
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets or sets the currently used <see cref="RouteTestContext"/>.
        /// </summary>
        /// <value>Result of <see cref="RouteTestContext"/> type.</value>
        protected RouteTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="IRouter"/> used in the route test.
        /// </summary>
        /// <value>Result of <see cref="IRouter"/> type.</value>
        protected IRouter Router => this.TestContext.Router;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> used in the route test.
        /// </summary>
        /// <value>Result of <see cref="IServiceProvider"/> type.</value>
        protected IServiceProvider Services => this.TestContext.Services;
    }
}
