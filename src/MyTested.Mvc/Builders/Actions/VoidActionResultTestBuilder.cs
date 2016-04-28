namespace MyTested.Mvc.Builders.Actions
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
        /// Initializes a new instance of the <see cref="VoidActionResultTestBuilder" /> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        public VoidActionResultTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithInvokedAction ShouldReturnEmpty()
        {
            CommonValidator.CheckForException(this.CaughtException);
            return this.NewAndProvideTestBuilder();
        }

        /// <inheritdoc />
        public IShouldHaveTestBuilder<VoidActionResult> ShouldHave()
        {
            this.TestContext.ActionResult = VoidActionResult.Instance;
            return new ShouldHaveTestBuilder<VoidActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IShouldThrowTestBuilder ShouldThrow()
        {
            return new ShouldThrowTestBuilder(this.TestContext);
        }
    }
}
