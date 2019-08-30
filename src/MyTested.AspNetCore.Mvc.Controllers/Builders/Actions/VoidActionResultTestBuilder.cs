namespace MyTested.AspNetCore.Mvc.Builders.Actions
{
    using Base;
    using Contracts.Actions;
    using Contracts.Base;
    using Contracts.CaughtExceptions;
    using CaughtExceptions;
    using Internal;
    using Internal.Results;
    using Internal.TestContexts;
    using ShouldHave;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public class VoidActionResultTestBuilder : BaseTestBuilderWithInvokedAction, IVoidActionResultTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidActionResultTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public VoidActionResultTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithInvokedAction ShouldReturnEmpty()
        {
            TestHelper.ExecuteTestCleanup();
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return this.NewAndTestBuilderWithInvokedAction();
        }

        /// <inheritdoc />
        public IShouldHaveTestBuilder<MethodResult> ShouldHave()
        {
            InvocationValidator.CheckForException(this.CaughtException, this.TestContext.ExceptionMessagePrefix);
            return new ShouldHaveTestBuilder<MethodResult>(this.TestContext);
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
