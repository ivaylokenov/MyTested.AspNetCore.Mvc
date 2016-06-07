namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base interface for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model used in action.</typeparam>
    public interface IBaseTestBuilderWithModel<TModel> : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithModel{TModel}"/> type.</returns>
        new IShouldPassForTestBuilderWithModel<TModel> ShouldPassFor();
    }
}
