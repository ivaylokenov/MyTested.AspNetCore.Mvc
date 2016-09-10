namespace MyTested.AspNetCore.Mvc.Builders.Actions
{
    using Base;
    using Contracts.Actions;
    using Contracts.CaughtExceptions;
    using CaughtExceptions;
    using Internal;
    using Internal.TestContexts;
    using ShouldHave;
    using ShouldReturn;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public class ActionResultTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IActionResultTestBuilder<TActionResult>
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
        public IShouldHaveTestBuilder<TActionResult> ShouldHave()
        {
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ShouldHaveTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
        {
            TestHelper.ExecuteTestCleanup();
            InvocationValidator.CheckForNullException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ShouldThrowTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldReturnTestBuilder<TActionResult> ShouldReturn()
        {
            TestHelper.ExecuteTestCleanup();
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ShouldReturnTestBuilder<TActionResult>(this.TestContext);
        }
    }
}
