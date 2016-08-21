namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;

    /// <summary>
    /// Base class for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public abstract class BaseTestBuilderWithModel<TModel> : BaseTestBuilderWithInvokedAction, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithModel{TModel}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithModel(ControllerTestContext testContext)
            : base(testContext)
        {
        }
    }
}
