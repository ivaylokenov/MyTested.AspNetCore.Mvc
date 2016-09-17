namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;
    using Licensing;
    using Microsoft.AspNetCore.Http;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    public abstract class BaseTestBuilder : IBaseTestBuilder
    {
        private HttpTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="HttpTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilder(HttpTestContext testContext)
        {
            TestCounter.IncrementAndValidate();
            this.TestContext = testContext;
        }

        public HttpContext HttpContext => this.TestContext.HttpContext;

        /// <summary>
        /// Gets the currently used <see cref="HttpTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="HttpTestContext"/>.</value>
        public HttpTestContext TestContext
        {
            get
            {
                return this.testContext;
            }
            
            private set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                CommonValidator.CheckForNullReference(value.HttpContext, nameof(this.HttpContext));
                this.testContext = value;
            }
        }
    }
}
