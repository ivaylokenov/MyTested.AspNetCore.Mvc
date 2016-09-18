namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilderWithModelError
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder"/> type.</returns>
        IModelErrorDetailsTestBuilder ContainingError(string errorKey);

        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> does not contain error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</returns>
        IAndModelErrorTestBuilder ContainingNoError(string errorKey);
    }
}
