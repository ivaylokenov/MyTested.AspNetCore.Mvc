namespace MyTested.Mvc
{
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Builders.Models;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IModelErrorTestBuilder"/>.
    /// </summary>
    public static class ModelErrorTestBuilderDataAnnotationsExtensions
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> is valid.
        /// </summary>
        /// <param name="modelErrorTestBuilder">Instance of <see cref="IModelErrorTestBuilder"/> type.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithInvokedAction"/> type.</returns>
        public static IBaseTestBuilderWithInvokedAction ContainingNoErrors(this IModelErrorTestBuilder modelErrorTestBuilder)
        {
            var actualModelErrorTestBuilder = (ModelErrorTestBuilder)modelErrorTestBuilder;

            actualModelErrorTestBuilder.CheckValidModelState();

            return actualModelErrorTestBuilder.NewAndProvideTestBuilder();
        }
    }
}
