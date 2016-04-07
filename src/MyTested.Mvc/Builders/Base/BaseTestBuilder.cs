namespace MyTested.Mvc.Builders.Base
{
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using ShouldPassFor;
    using Utilities.Validators;

    public abstract class BaseTestBuilder : IBaseTestBuilder
    {
        private HttpTestContext testContext;

        protected BaseTestBuilder(HttpTestContext testContext)
        {
            this.TestContext = testContext;
        }

        internal HttpContext HttpContext => this.TestContext.HttpContext;

        protected HttpTestContext TestContext
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

        public IShouldPassForTestBuilder ShouldPassFor() => new ShouldPassForTestBuilder(this.TestContext);
    }
}
