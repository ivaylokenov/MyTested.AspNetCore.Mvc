namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Base
{
    using Builders.Base;
    using Contracts.ActionResults.Base;
    using Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    
    /// <summary>
    /// Base class for all test builders with output <see cref="ActionResult"/>.
    /// </summary>
    /// <typeparam name="TOutputResult">Output result from invoked action in ASP.NET Core MVC controller.</typeparam>
    /// <typeparam name="TOutputResultTestBuilder">Type of output result test builder to use as a return type for common methods.</typeparam>
    public abstract class BaseTestBuilderWithOutputResult<TOutputResult, TOutputResultTestBuilder>
        : BaseTestBuilderWithResponseModel<TOutputResult>, 
        IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder>,
        IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder> 
        where TOutputResult : class
        where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BaseTestBuilderWithOutputResult{TOutputResult, TOutputResultTestBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithOutputResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the output result test builder.
        /// </summary>
        /// <value>Test builder for the output <see cref="ActionResult"/>.</value>
        public abstract TOutputResultTestBuilder ResultTestBuilder { get; }
    }
}
