namespace MyTested.Mvc.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public interface IBaseTestBuilderWithModel<out TModel> : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Gets the model returned from an action result.
        /// </summary>
        /// <returns>Model returned from action result.</returns>
        TModel AndProvideTheModel();
    }
}
