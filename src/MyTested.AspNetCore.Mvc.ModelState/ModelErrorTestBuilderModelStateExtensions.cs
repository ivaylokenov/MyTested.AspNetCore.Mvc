namespace MyTested.AspNetCore.Mvc
{
    using Builders.And;
    using Builders.Contracts.And;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IModelErrorTestBuilder"/>.
    /// </summary>
    public static class ModelErrorTestBuilderModelStateExtensions
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> is valid.
        /// </summary>
        /// <param name="modelErrorTestBuilder">Instance of <see cref="IModelErrorTestBuilder"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ContainingNoErrors(this IModelErrorTestBuilder modelErrorTestBuilder)
        {
            var actualModelErrorTestBuilder = (ModelErrorTestBuilder)modelErrorTestBuilder;

            ModelStateValidator.CheckValidModelState(actualModelErrorTestBuilder.TestContext);

            return new AndTestBuilder(actualModelErrorTestBuilder.TestContext);
        }
    }
}
