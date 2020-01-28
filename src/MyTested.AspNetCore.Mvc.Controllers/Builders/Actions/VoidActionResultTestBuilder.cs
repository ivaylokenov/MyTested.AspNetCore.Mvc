namespace MyTested.AspNetCore.Mvc.Builders.Actions
{
    using Contracts.Actions;
    using Contracts.Base;
    using Internal;
    using Internal.Results;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public class VoidActionResultTestBuilder 
        : BaseActionResultTestBuilder<MethodResult>, 
        IVoidActionResultTestBuilder
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
    }
}
