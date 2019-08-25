namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Internal;
    using Utilities.Extensions;

    /// <summary>
    /// Contains view features extension methods for <see cref="IModelErrorTestBuilder{TModel}"/>.
    /// </summary>
    public static class ModelErrorTestBuilderViewFeaturesExtensions
    {
        private static readonly dynamic expressionHelper = WebFramework.Internals.ExpressionHelper.Exposed();

        private static readonly IDictionary<LambdaExpression, string> expressionTextCache
            = new ConcurrentDictionary<LambdaExpression, string>(WebFramework.Internals.LambdaExpressionComparer.Exposed().Instance);

        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by member expression.
        /// </summary>
        /// <typeparam name="TModel">Type of model which will be tested for no errors.</typeparam>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="modelErrorTestBuilder">Instance of <see cref="IModelErrorTestBuilder{TModel}"/> type.</param>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</returns>
        public static IModelErrorDetailsTestBuilder<TModel> ContainingErrorFor<TModel, TMember>(
            this IModelErrorTestBuilder<TModel> modelErrorTestBuilder,
            Expression<Func<TModel, TMember>> memberWithError)
        {
            var actualModelErrorTestBuilder = (ModelErrorTestBuilder<TModel>)modelErrorTestBuilder;

            var memberName = expressionHelper.GetExpressionText(memberWithError, expressionTextCache);
            actualModelErrorTestBuilder.ContainingError(memberName);

            return new ModelErrorDetailsTestBuilder<TModel>(
                actualModelErrorTestBuilder.TestContext,
                actualModelErrorTestBuilder,
                memberName,
                actualModelErrorTestBuilder.ModelState[memberName].Errors);
        }

        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains no error by member expression.
        /// </summary>
        /// <typeparam name="TModel">Type of model which will be tested for no errors.</typeparam>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="modelErrorTestBuilder">Instance of <see cref="IModelErrorTestBuilder{TModel}"/> type.</param>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>The same <see cref="IAndModelErrorTestBuilder{TModel}"/>.</returns>
        public static IAndModelErrorTestBuilder<TModel> ContainingNoErrorFor<TModel, TMember>(
            this IModelErrorTestBuilder<TModel> modelErrorTestBuilder,
            Expression<Func<TModel, TMember>> memberWithNoError)
        {
            var actualModelErrorTestBuilder = (ModelErrorTestBuilder<TModel>)modelErrorTestBuilder;

            var memberName = expressionHelper.GetExpressionText(memberWithNoError, expressionTextCache);
            if (actualModelErrorTestBuilder.ModelState.ContainsKey(memberName))
            {
                actualModelErrorTestBuilder.ThrowNewModelErrorAssertionException(
                    "{0} to have no model errors against key {1}, but found some.",
                    memberName);
            }

            return actualModelErrorTestBuilder;
        }
    }
}
