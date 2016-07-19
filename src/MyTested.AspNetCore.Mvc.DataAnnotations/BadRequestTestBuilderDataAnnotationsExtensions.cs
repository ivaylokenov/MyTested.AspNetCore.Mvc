namespace MyTested.AspNetCore.Mvc
{
    using System.Collections.Generic;
    using System.Linq;
    using Builders.ActionResults.BadRequest;
    using Builders.Base;
    using Builders.Contracts.ActionResults.BadRequest;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;

    /// <summary>
    /// Contains <see cref="ModelStateDictionary"/> extension methods for <see cref="IBadRequestTestBuilder"/>.
    /// </summary>
    public static class BadRequestTestBuilderDataAnnotationsExtensions
    {
        /// <summary>
        /// Tests whether <see cref="BadRequestObjectResult"/> contains the controller's <see cref="ModelStateDictionary"/> as object error.
        /// </summary>
        /// <param name="badRequestTestBuilder">Instance of <see cref="IBadRequestTestBuilder"/> type.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithModelStateError(this IBadRequestTestBuilder badRequestTestBuilder)
        {
            var actualBadRequestTestBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);
            return actualBadRequestTestBuilder
                .WithModelStateError(actualBadRequestTestBuilder.TestContext.ModelState);
        }

        /// <summary>
        /// Tests whether <see cref="BadRequestObjectResult"/> contains specific <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <param name="badRequestTestBuilder">Instance of <see cref="IBadRequestTestBuilder"/> type.</param>
        /// <param name="modelState"><see cref="ModelStateDictionary"/> to deeply compare to the actual one.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithModelStateError(
            this IBadRequestTestBuilder badRequestTestBuilder,
            ModelStateDictionary modelState)
        {
            var actualBadRequestTestBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            var badRequestObjectResultValue = actualBadRequestTestBuilder.GetBadRequestObjectResultValue();
            var actualModelState = actualBadRequestTestBuilder.GetModelStateFromSerializableError(badRequestObjectResultValue);

            var modelStateKeys = modelState.Keys.ToList();
            var actualModelStateKeys = actualModelState.Keys.ToList();

            var expectedKeysCount = modelStateKeys.Count;
            var actualKeysCount = actualModelStateKeys.Count;

            if (expectedKeysCount != actualKeysCount)
            {
                throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} keys, but found {3}.",
                        actualBadRequestTestBuilder.ActionName,
                        actualBadRequestTestBuilder.Component.GetName(),
                        expectedKeysCount,
                        actualKeysCount));
            }

            var expectedModelStateSortedKeys = modelStateKeys.OrderBy(k => k).ToList();

            foreach (var expectedKey in expectedModelStateSortedKeys)
            {
                if (!actualModelState.ContainsKey(expectedKey))
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} key, but none found.",
                        actualBadRequestTestBuilder.ActionName,
                        actualBadRequestTestBuilder.Component.GetName(),
                        expectedKey));
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} errors for {3} key, but found {4}.",
                        actualBadRequestTestBuilder.ActionName,
                        actualBadRequestTestBuilder.Component.GetName(),
                        expectedSortedErrors.Count,
                        expectedKey,
                        actualSortedErrors.Count));
                }

                for (int i = 0; i < expectedSortedErrors.Count; i++)
                {
                    var expectedError = expectedSortedErrors[i];
                    var actualError = actualSortedErrors[i];
                    actualBadRequestTestBuilder.ValidateErrorMessage(expectedError, actualError);
                }
            }

            return actualBadRequestTestBuilder;
        }

        /// <summary>
        /// Tests whether <see cref="BadRequestObjectResult"/> contains specific model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <param name="badRequestTestBuilder">Instance of <see cref="IBadRequestTestBuilder"/> type.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IModelErrorTestBuilder<TRequestModel> WithModelStateErrorFor<TRequestModel>(this IBadRequestTestBuilder badRequestTestBuilder)
        {
            var actualBadRequestTestBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            actualBadRequestTestBuilder.TestContext.Model = actualBadRequestTestBuilder.GetBadRequestObjectResultValue();

            return new ModelErrorTestBuilder<TRequestModel>(
                actualBadRequestTestBuilder.TestContext,
                modelState: actualBadRequestTestBuilder.GetModelStateFromSerializableError(actualBadRequestTestBuilder.TestContext.Model));
        }

        private static BadRequestTestBuilder<BadRequestObjectResult> GetBadRequestTestBuilder(IBadRequestTestBuilder badRequestTestBuilder)
        {
            var actualBadRequestTestBuilder = badRequestTestBuilder as BadRequestTestBuilder<BadRequestObjectResult>;

            if (actualBadRequestTestBuilder == null)
            {
                var badRequestTestBuilderBase = (BaseTestBuilderWithInvokedAction)badRequestTestBuilder;

                throw new BadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request result to contain error object, but it could not be found.",
                    badRequestTestBuilderBase.ActionName,
                    badRequestTestBuilderBase.Component.GetName()));
            }

            return actualBadRequestTestBuilder;
        }

        private static IList<string> GetSortedErrorMessagesForModelStateKey(IEnumerable<ModelError> errors)
        {
            return errors
                .OrderBy(er => er.ErrorMessage)
                .Select(er => er.ErrorMessage)
                .ToList();
        }
    }
}
