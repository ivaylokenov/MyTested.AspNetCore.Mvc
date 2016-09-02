namespace MyTested.AspNetCore.Mvc
{
    using System;
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
                    "{0} bad request model state dictionary to contain {1} keys, but found {2}.",
                    actualBadRequestTestBuilder.TestContext.ExceptionMessagePrefix,
                    expectedKeysCount,
                    actualKeysCount));
            }

            var expectedModelStateSortedKeys = modelStateKeys.OrderBy(k => k).ToList();

            foreach (var expectedKey in expectedModelStateSortedKeys)
            {
                if (!actualModelState.ContainsKey(expectedKey))
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "{0} bad request model state dictionary to contain {1} key, but none found.",
                        actualBadRequestTestBuilder.TestContext.ExceptionMessagePrefix,
                        expectedKey));
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "{0} bad request model state dictionary to contain {1} errors for {2} key, but found {3}.",
                        actualBadRequestTestBuilder.TestContext.ExceptionMessagePrefix,
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
        /// <param name="badRequestTestBuilder">Instance of <see cref="IBadRequestTestBuilder"/> type.</param>
        /// <param name="modelStateTestBuilder">Model state errors test builder.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithModelStateError(
            this IBadRequestTestBuilder badRequestTestBuilder,
            Action<IModelStateTestBuilder> modelStateTestBuilder)
        {
            var actualBadRequestTestBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            actualBadRequestTestBuilder.TestContext.Model = actualBadRequestTestBuilder.GetBadRequestObjectResultValue();

            var newModelStateTestBuilder = new ModelStateTestBuilder(
                actualBadRequestTestBuilder.TestContext,
                modelState: actualBadRequestTestBuilder.GetModelStateFromSerializableError(actualBadRequestTestBuilder.TestContext.Model));

            modelStateTestBuilder(newModelStateTestBuilder);

            return actualBadRequestTestBuilder;
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
                    badRequestTestBuilderBase.Controller.GetName()));
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
