namespace MyTested.AspNetCore.Mvc.Builders.And
{
    using Base;
    using Internal.TestContexts;

    /// <summary>
    /// Provides additional testing methods.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public class AndTestBuilderWithViewComponentResult<TInvocationResult> : BaseTestBuilderWithViewComponentResult<TInvocationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndTestBuilderWithViewComponentResult{TInvocationResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ViewComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public AndTestBuilderWithViewComponentResult(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
