namespace MyTested.Mvc
{
    using System;
    using System.Linq.Expressions;
    using Builders.Contracts.Models;
    using Builders.Models;

    /// <summary>
    /// Contains view features extension methods for <see cref="IModelErrorDetailsTestBuilder{TModel}"/>.
    /// </summary>
    public static class ModelErrorDetailsTestBuilderViewFeaturesExtensions
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by member expression.
        /// </summary>
        /// <typeparam name="TModel">Type of model which will be tested for no errors.</typeparam>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="modelErrorDetailsTestBuilder">Instance of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</param>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>The same <see cref="IModelErrorDetailsTestBuilder{TModel}"/>.</returns>
        public static IModelErrorDetailsTestBuilder<TModel> ContainingErrorFor<TModel, TMember>(
            this IModelErrorDetailsTestBuilder<TModel> modelErrorDetailsTestBuilder,
            Expression<Func<TModel, TMember>> memberWithError)
        {
            return ((ModelErrorDetailsTestBuilder<TModel>)modelErrorDetailsTestBuilder)
                .ModelErrorTestBuilder
                .ContainingErrorFor(memberWithError);
        }

        /// <summary>
        /// Tests whether tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains no error by member expression.
        /// </summary>
        /// <typeparam name="TModel">Type of model which will be tested for no errors.</typeparam>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="modelErrorDetailsTestBuilder">Instance of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</param>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>Test builder of <see cref="IModelErrorTestBuilder{TModel}"/> type.</returns>
        public static IModelErrorTestBuilder<TModel> ContainingNoErrorFor<TModel, TMember>(
            this IModelErrorDetailsTestBuilder<TModel> modelErrorDetailsTestBuilder,
            Expression<Func<TModel, TMember>> memberWithNoError)
        {
            return ((ModelErrorDetailsTestBuilder<TModel>)modelErrorDetailsTestBuilder)
                .ModelErrorTestBuilder
                .ContainingNoErrorFor(memberWithNoError);
        }
    }
}
