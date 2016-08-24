namespace MyTested.AspNetCore.Mvc.Builders.And
{
    using Base;
    using Contracts.And;
    using Internal.TestContexts;
    using Contracts.Base;

    /// <summary>
    /// Provides additional testing methods.
    /// </summary>
    public class AndTestBuilderWithInvokedAction : BaseTestBuilderWithInvokedAction, IAndTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndTestBuilderWithInvokedAction"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AndTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithInvokedAction AndAlso() => this;
    }
}
