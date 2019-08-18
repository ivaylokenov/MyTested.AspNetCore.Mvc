namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithErrorResultDataAnnotationsExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> contains the
        /// controller's <see cref="ModelStateDictionary"/> as object error.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithModelStateError<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            return baseTestBuilderWithErrorResult
                .WithModelStateError(actualBuilder.TestContext.ModelState);
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> contains specific <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <param name="modelState"><see cref="ModelStateDictionary"/> to deeply compare to the actual one.</param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithModelStateError<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult,
            ModelStateDictionary modelState)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);
            
            var actualModelState = actualBuilder.GetModelStateFromSerializableError();

            var modelStateKeys = modelState.Keys.ToList();
            var actualModelStateKeys = actualModelState.Keys.ToList();

            var expectedKeysCount = modelStateKeys.Count;
            var actualKeysCount = actualModelStateKeys.Count;

            if (expectedKeysCount != actualKeysCount)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "model state dictionary",
                    $"to contain {expectedKeysCount} keys",
                    $"instead found {actualKeysCount}");
            }

            var expectedModelStateSortedKeys = modelStateKeys.OrderBy(k => k).ToList();

            foreach (var expectedKey in expectedModelStateSortedKeys)
            {
                if (!actualModelState.ContainsKey(expectedKey))
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "model state dictionary",
                        $"to contain '{expectedKey}' key",
                        "it was not found");
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "model state dictionary",
                        $"to contain {expectedSortedErrors.Count} errors for the '{expectedKey}' key",
                        $"instead found {actualSortedErrors.Count}");
                }

                for (int i = 0; i < expectedSortedErrors.Count; i++)
                {
                    var expectedError = expectedSortedErrors[i];
                    var actualError = actualSortedErrors[i];
                    actualBuilder.ValidateErrorMessage(expectedError, actualError);
                }
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> contains specific model state errors by using a test builder.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <param name="modelStateTestBuilder">Model state errors test builder.</param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithModelStateError<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult,
            Action<IModelStateTestBuilder> modelStateTestBuilder)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            actualBuilder.TestContext.Model = actualBuilder.GetObjectResultValue();

            var newModelStateTestBuilder = new ModelStateTestBuilder(
                actualBuilder.TestContext,
                actualBuilder.GetModelStateFromSerializableError(actualBuilder.TestContext.Model));

            modelStateTestBuilder(newModelStateTestBuilder);

            return actualBuilder.ResultTestBuilder;
        }

        private static IList<string> GetSortedErrorMessagesForModelStateKey(IEnumerable<ModelError> errors)
            => errors
                .OrderBy(er => er.ErrorMessage)
                .Select(er => er.ErrorMessage)
                .ToList();

        private static IBaseTestBuilderWithErrorResultInternal<TErrorResultTestBuilder>
            GetActualBuilder<TErrorResultTestBuilder>(
                IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder =
                (IBaseTestBuilderWithErrorResultInternal<TErrorResultTestBuilder>)baseTestBuilderWithErrorResult;

            if (!(actualBuilder.TestContext.MethodResult is ObjectResult))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "to",
                    "contain error object",
                    "such was not be found");
            }

            return actualBuilder;
        }
    }
}
