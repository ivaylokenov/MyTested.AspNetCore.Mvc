namespace MyTested.Mvc.Builders.And
{
    using Base;
    using Internal.TestContexts;

    /// <summary>
    /// Provides controller and action information.
    /// </summary>
    public class AndProvideTestBuilder : BaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndProvideTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        public AndProvideTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
    }
}
