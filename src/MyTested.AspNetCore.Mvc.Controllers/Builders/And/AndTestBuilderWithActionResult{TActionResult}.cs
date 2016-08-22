namespace MyTested.AspNetCore.Mvc.Builders.And
{
    using Base;
    using Internal.TestContexts;

    /// <summary>
    /// Provides additional testing methods.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class AndTestBuilderWithActionResult<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndTestBuilderWithActionResult{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AndTestBuilderWithActionResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }
    }
}
