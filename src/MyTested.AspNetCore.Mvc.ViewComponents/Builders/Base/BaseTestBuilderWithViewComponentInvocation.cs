namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using Contracts.Base;
    using Internal.TestContexts;

    public class BaseTestBuilderWithViewComponentInvocation : BaseTestBuilderWithViewComponent,
        IBaseTestBuilderWithViewComponentInvocation
    {
        public BaseTestBuilderWithViewComponentInvocation(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <summary>
        /// Gets the caught exception. Returns null, if such does not exist.
        /// </summary>
        /// <value>Result of type <see cref="Exception"/>.</value>
        public Exception CaughtException => this.TestContext.CaughtException;
    }
}
