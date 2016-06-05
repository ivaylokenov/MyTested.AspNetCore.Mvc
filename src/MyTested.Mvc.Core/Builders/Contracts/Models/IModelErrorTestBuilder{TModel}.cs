namespace MyTested.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelErrorTestBuilder<TModel> : IModelErrorTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey);
    }
}
