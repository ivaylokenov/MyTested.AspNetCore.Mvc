namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Contracts.ActionResults.BadRequest;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using static BadRequestTestBuilderExtensions;

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
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);
            return actualBuilder.WithModelStateError(actualBuilder.TestContext.ModelState);
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
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            var badRequestObjectResultValue = actualBuilder.GetBadRequestObjectResultValue();
            var actualModelState = actualBuilder.GetModelStateFromSerializableError(badRequestObjectResultValue);

            var modelStateKeys = modelState.Keys.ToList();
            var actualModelStateKeys = actualModelState.Keys.ToList();

            var expectedKeysCount = modelStateKeys.Count;
            var actualKeysCount = actualModelStateKeys.Count;

            if (expectedKeysCount != actualKeysCount)
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "{0} bad request model state dictionary to contain {1} keys, but found {2}.",
                    actualBuilder.TestContext.ExceptionMessagePrefix,
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
                        actualBuilder.TestContext.ExceptionMessagePrefix,
                        expectedKey));
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "{0} bad request model state dictionary to contain {1} errors for {2} key, but found {3}.",
                        actualBuilder.TestContext.ExceptionMessagePrefix,
                        expectedSortedErrors.Count,
                        expectedKey,
                        actualSortedErrors.Count));
                }

                for (int i = 0; i < expectedSortedErrors.Count; i++)
                {
                    var expectedError = expectedSortedErrors[i];
                    var actualError = actualSortedErrors[i];
                    actualBuilder.ValidateErrorMessage(expectedError, actualError);
                }
            }

            return actualBuilder;
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
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            actualBuilder.TestContext.Model = actualBuilder.GetBadRequestObjectResultValue();

            var newModelStateTestBuilder = new ModelStateTestBuilder(
                actualBuilder.TestContext,
                modelState: actualBuilder.GetModelStateFromSerializableError(actualBuilder.TestContext.Model));

            modelStateTestBuilder(newModelStateTestBuilder);

            return actualBuilder;
        }

        private static IList<string> GetSortedErrorMessagesForModelStateKey(IEnumerable<ModelError> errors) 
            => errors
                .OrderBy(er => er.ErrorMessage)
                .Select(er => er.ErrorMessage)
                .ToList();
    }
}
