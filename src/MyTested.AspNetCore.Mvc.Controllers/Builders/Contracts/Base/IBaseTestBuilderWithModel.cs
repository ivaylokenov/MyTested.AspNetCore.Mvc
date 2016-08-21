namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model used in action.</typeparam>
    public interface IBaseTestBuilderWithModel<TModel> : IBaseTestBuilderWithInvokedAction
    {
    }
}
