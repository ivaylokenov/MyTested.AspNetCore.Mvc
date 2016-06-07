namespace MyTested.AspNetCore.Mvc.Builders.And
{
    using Base;
    using Internal.TestContexts;

    /// <summary>
    /// Provides controller and action information.
    /// </summary>
    public class AndProvideTestBuilder : BaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndProvideTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AndProvideTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
    }
}
