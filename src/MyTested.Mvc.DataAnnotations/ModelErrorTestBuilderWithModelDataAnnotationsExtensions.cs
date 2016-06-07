namespace MyTested.Mvc
{
    using Builders.Contracts.Models;
    using Builders.Models;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IModelErrorTestBuilder{TModel}"/>.
    /// </summary>
    public static class ModelErrorTestBuilderWithModelDataAnnotationsExtensions
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by key.
        /// </summary>
        /// <param name="modelErrorTestBuilder">Instance of <see cref="IModelErrorTestBuilder{TModel}"/> type.</param>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</returns>
        public static IModelErrorDetailsTestBuilder<TModel> ContainingError<TModel>(
            this IModelErrorTestBuilder<TModel> modelErrorTestBuilder,
            string errorKey)
        {
            var actualModelErrorTestBuilder = (ModelErrorTestBuilder<TModel>)modelErrorTestBuilder;

            if (!actualModelErrorTestBuilder.ModelState.ContainsKey(errorKey)
                || actualModelErrorTestBuilder.ModelState.Count == 0)
            {
                actualModelErrorTestBuilder.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have a model error against key {2}, but none found.",
                    errorKey);
            }

            return new ModelErrorDetailsTestBuilder<TModel>(
                actualModelErrorTestBuilder.TestContext,
                actualModelErrorTestBuilder,
                errorKey,
                actualModelErrorTestBuilder.ModelState[errorKey].Errors);
        }

    }
}
