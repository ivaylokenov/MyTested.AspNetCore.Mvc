namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.ActionResult
{
    using Builders.Base;
    using Contracts.ActionResults.ActionResult;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ActionResult{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">Type of the expected result.</typeparam>
    public class ActionResultOfTTestBuilder<TResult>
        : BaseTestBuilderWithActionResult<TResult>,
        IAndActionResultOfTTestBuilder<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultOfTTestBuilder{TResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public ActionResultOfTTestBuilder(ControllerTestContext testContext) 
            : base(testContext)
        {
            testContext.ConvertMethodResult();
        }

        /// <inheritdoc />
        public IActionResultOfTTestBuilder<TResult> AndAlso() => this;
    }
}
