namespace MyTested.AspNetCore.Mvc.Builders.And
{
    using Base;
    using Internal.TestContexts;

    /// <summary>
    /// Provides additional testing methods.
    /// </summary>
    public class AndTestBuilderWithInvokedAction : BaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndTestBuilderWithInvokedAction"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AndTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }
    }
}
