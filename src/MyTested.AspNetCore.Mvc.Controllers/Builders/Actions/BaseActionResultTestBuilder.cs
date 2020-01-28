namespace MyTested.AspNetCore.Mvc.Builders.Actions
{
    using Base;
    using CaughtExceptions;
    using Contracts.Actions;
    using Contracts.CaughtExceptions;
    using Internal;
    using Internal.TestContexts;
    using ShouldHave;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all action result test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseActionResultTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>,
        IBaseActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseActionResultTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseActionResultTestBuilder(ControllerTestContext testContext)
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
    }
}
