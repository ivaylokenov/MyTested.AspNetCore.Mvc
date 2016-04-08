namespace MyTested.Mvc.Builders.Base
{
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using ShouldPassFor;

    /// <summary>
    /// Base class for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public abstract class BaseTestBuilderWithModel<TModel> : BaseTestBuilderWithInvokedAction, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithModel{TModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="model">Model returned from action result.</param>
        protected BaseTestBuilderWithModel(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        public new IShouldPassForTestBuilderWithModel<TModel> ShouldPassFor()
        {
            return new ShouldPassForTestBuilderWithModel<TModel>(this.TestContext);
        }
    }
}
