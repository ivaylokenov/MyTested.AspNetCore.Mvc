namespace MyTested.Mvc.Builders.Actions
{
    using Base;
    using Contracts.Actions;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using ShouldHave;
    using ShouldReturn;
    using Utilities.Extensions;
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
            CommonValidator.CheckForException(this.CaughtException);
            return new ShouldHaveTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
        {
            TestHelper.ExecuteTestCleanup();
            if (this.CaughtException == null)
            {
                throw new ActionCallAssertionException(string.Format(
                    "When calling {0} action in {1} thrown exception was expected, but in fact none was caught.",
                    this.ActionName,
                    this.Controller.GetName()));
            }
            
            return new ShouldThrowTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldReturnTestBuilder<TActionResult> ShouldReturn()
        {
            TestHelper.ExecuteTestCleanup();
            CommonValidator.CheckForException(this.CaughtException);
            return new ShouldReturnTestBuilder<TActionResult>(this.TestContext);
        }
    }
}
