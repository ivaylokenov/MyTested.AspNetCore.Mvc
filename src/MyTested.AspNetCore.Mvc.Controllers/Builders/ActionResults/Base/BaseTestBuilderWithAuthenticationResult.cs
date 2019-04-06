namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Base
{
    using Builders.Base;
    using Contracts.ActionResults.Base;
    using Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base class for all test builders with authentication action result.
    /// </summary>
    /// <typeparam name="TAuthenticationResult">
    /// Authentication action result from invoked action in ASP.NET Core MVC controller.
    /// </typeparam>
    /// <typeparam name="TAuthenticationResultTestBuilder">
    /// Type of authentication result test builder to use as a return type for common methods.
    /// </typeparam>
    public abstract class BaseTestBuilderWithAuthenticationResult<TAuthenticationResult, TAuthenticationResultTestBuilder>
        : BaseTestBuilderWithActionResult<TAuthenticationResult>, 
        IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder>,
        IBaseTestBuilderWithAuthenticationResultInternal<TAuthenticationResultTestBuilder>
        where TAuthenticationResult : ActionResult
        where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BaseTestBuilderWithAuthenticationResult{TAuthenticationResult, TAuthenticationResultTestBuilder}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        protected BaseTestBuilderWithAuthenticationResult(ControllerTestContext testContext) 
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the authentication action result test builder.
        /// </summary>
        /// <value>Test builder for the authentication action result.</value>
        public abstract TAuthenticationResultTestBuilder ResultTestBuilder { get; }
        
        public abstract void ThrowNewAuthenticationResultAssertionException(
            string propertyName, 
            string expectedValue,
            string actualValue);
    }
}
