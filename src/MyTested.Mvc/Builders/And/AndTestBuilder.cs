namespace MyTested.Mvc.Builders.And
{
    using Actions;
    using Base;
    using Contracts.Actions;
    using Contracts.And;
    using Internal;
    using Internal.TestContexts;

    /// <summary>
    /// Class containing AndAlso() method allowing additional assertions after action tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class AndTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>,
        IAndTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AndTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            TestHelper.ClearMemoryCache();
        }

        /// <inheritdoc />
        public IActionResultTestBuilder<TActionResult> AndAlso()
        {
            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }
    }
}
