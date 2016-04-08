namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model used in action.</typeparam>
    public interface IBaseTestBuilderWithModel<TModel> : IBaseTestBuilderWithInvokedAction
    {
        new IShouldPassForTestBuilderWithModel<TModel> ShouldPassFor();
    }
}
