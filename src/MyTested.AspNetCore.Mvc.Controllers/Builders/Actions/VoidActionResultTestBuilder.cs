namespace MyTested.AspNetCore.Mvc.Builders.Actions
{
    using Base;
    using Contracts.Actions;
    using Contracts.Base;
    using Internal;
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
            ActionValidator.CheckForException(this.CaughtException);
            return this.NewAndTestBuilderWithInvokedAction();
        }

        /// <inheritdoc />
        public IShouldHaveTestBuilder<VoidMethodResult> ShouldHave()
        {
            return new ShouldHaveTestBuilder<VoidMethodResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
        {
            return new ShouldThrowTestBuilder(this.TestContext);
        }
    }
}
