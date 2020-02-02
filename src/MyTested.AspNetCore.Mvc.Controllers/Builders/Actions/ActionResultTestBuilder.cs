namespace MyTested.AspNetCore.Mvc.Builders.Actions
{
    using Contracts.Actions;
    using Internal;
    using Internal.TestContexts;
    using ShouldReturn;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ActionResultTestBuilder<TActionResult>
        : BaseActionResultTestBuilder<TActionResult>, 
        IActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ActionResultTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldReturnActionResultTestBuilder<TActionResult> ShouldReturn()
        {
            TestHelper.ExecuteTestCleanup();
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ShouldReturnActionResultTestBuilder<TActionResult>(this.TestContext);
        }
    }
}
