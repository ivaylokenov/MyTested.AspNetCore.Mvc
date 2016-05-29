namespace MyTested.Mvc.Builders.ActionResults.BadRequest
{
    using Base;
    using Contracts.Base;
    using Contracts.ActionResults.BadRequest;
    using Contracts.Models;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using ShouldPassFor;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing bad request results.
    /// </summary>
    /// <typeparam name="THttpBadRequestResult">Type of bad request result - <see cref="BadRequestResult"/> or <see cref="BadRequestObjectResult"/>.</typeparam>
    public class BadRequestTestBuilder<THttpBadRequestResult>
        : BaseTestBuilderWithResponseModel<THttpBadRequestResult>, IAndBadRequestTestBuilder
        where THttpBadRequestResult : ActionResult
    {
        private const string ErrorMessage = "When calling {0} action in {1} expected bad request result error to be the given object, but in fact it was a different.";
        private const string OfTypeErrorMessage = "When calling {0} action in {1} expected bad request result error to be of {2} type, but instead received {3}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestTestBuilder{TBadRequestResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public BadRequestTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.ErrorMessageFormat = ErrorMessage;
            this.OfTypeErrorMessageFormat = OfTypeErrorMessage;
        }
        
        /// <inheritdoc />
        public IModelDetailsTestBuilder<TError> WithError<TError>(TError error)
        {
            return this.WithResponseModel(error);
        }

        /// <inheritdoc />
        public IModelDetailsTestBuilder<TError> WithErrorOfType<TError>()
        {
            return this.WithResponseModelOfType<TError>();
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithNoError()
        {
            var actualResult = this.ActionResult as BadRequestResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request result to not have error message, but in fact such was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithStatusCode(int statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IBadRequestErrorMessageTestBuilder WithErrorMessage()
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            return new BadRequestErrorMessageTestBuilder(
                this.TestContext,
                actualErrorMessage,
                this);
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithErrorMessage(string error)
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            this.ValidateErrorMessage(error, actualErrorMessage);

            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithErrorMessage(Action<string> assertions)
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            assertions(actualErrorMessage);

            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithErrorMessage(Func<string, bool> predicate)
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            if (!predicate(actualErrorMessage))
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request error message ('{2}') to pass the given predicate, but it failed.",
                    this.ActionName,
                    this.Controller.GetName(),
                    actualErrorMessage));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithModelStateError()
        {
            return this.WithModelStateError(this.TestContext.ControllerContext.ModelState);
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithModelStateError(ModelStateDictionary modelState)
        {
            var badRequestObjectResultValue = this.GetBadRequestObjectResultValue();
            var actualModelState = this.GetModelStateFromSerializableError(badRequestObjectResultValue);

            var modelStateKeys = modelState.Keys.ToList();
            var actualModelStateKeys = actualModelState.Keys.ToList();

            var expectedKeysCount = modelStateKeys.Count;
            var actualKeysCount = actualModelStateKeys.Count;

            if (expectedKeysCount != actualKeysCount)
            {
                throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} keys, but found {3}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedKeysCount,
                        actualKeysCount));
            }

            var actualModelStateSortedKeys = actualModelStateKeys.OrderBy(k => k).ToList();
            var expectedModelStateSortedKeys = modelStateKeys.OrderBy(k => k).ToList();

            foreach (var expectedKey in expectedModelStateSortedKeys)
            {
                if (!actualModelState.ContainsKey(expectedKey))
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} key, but none found.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedKey));
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} errors for {3} key, but found {4}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedSortedErrors.Count,
                        expectedKey,
                        actualSortedErrors.Count));
                }

                for (int i = 0; i < expectedSortedErrors.Count; i++)
                {
                    var expectedError = expectedSortedErrors[i];
                    var actualError = actualSortedErrors[i];
                    this.ValidateErrorMessage(expectedError, actualError);
                }
            }

            return this;
        }

        /// <inheritdoc />
        public IModelErrorTestBuilder<TRequestModel> WithModelStateErrorFor<TRequestModel>()
        {
            this.TestContext.Model = this.GetBadRequestObjectResultValue();
            return new ModelErrorTestBuilder<TRequestModel>(
                this.TestContext,
                modelState: this.GetModelStateFromSerializableError(this.TestContext.Model));
        }

        /// <inheritdoc />
        public IBadRequestTestBuilder AndAlso() => this;

        IShouldPassForTestBuilderWithActionResult<ActionResult> IBaseTestBuilderWithActionResult<ActionResult>.ShouldPassFor()
            => new ShouldPassForTestBuilderWithActionResult<ActionResult>(this.TestContext);

        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewHttpBadRequestResultAssertionException(propertyName, expectedValue, actualValue);

        private static IList<string> GetSortedErrorMessagesForModelStateKey(IEnumerable<ModelError> errors)
        {
            return errors
                .OrderBy(er => er.ErrorMessage)
                .Select(er => er.ErrorMessage)
                .ToList();
        }

        private object GetBadRequestObjectResultValue()
        {
            var actualBadRequestResult = this.ActionResult as BadRequestObjectResult;
            if (actualBadRequestResult == null)
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request result to contain error object, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return actualBadRequestResult.Value;
        }

        private string GetBadRequestErrorMessage()
        {
            var errorMessage = this.GetBadRequestObjectResultValue() as string;
            if (errorMessage == null)
            {
                this.ThrowNewHttpBadRequestResultAssertionExceptionWithMessage();
            }

            return errorMessage;
        }

        private void ValidateErrorMessage(string expectedMessage, string actualMessage)
        {
            if (expectedMessage != actualMessage)
            {
                this.ThrowNewHttpBadRequestResultAssertionExceptionWithMessage($"message '{expectedMessage}'", $"'{actualMessage}'");
            }
        }

        private ModelStateDictionary GetModelStateFromSerializableError(object error)
        {
            var serializableError = error as SerializableError;
            if (serializableError == null)
            {
                this.ThrowNewHttpBadRequestResultAssertionExceptionWithMessage("model state dictionary as error", "other type of error");
            }

            var result = new ModelStateDictionary();

            foreach (var errorKeyValuePair in serializableError)
            {
                var errorKey = errorKeyValuePair.Key;
                var errorValues = errorKeyValuePair.Value as string[];
                if (errorValues != null)
                {
                    errorValues.ForEach(er => result.AddModelError(errorKey, er));
                }
            }

            return result;
        }

        private void ThrowNewHttpBadRequestResultAssertionExceptionWithMessage(string expectedMessage = null, string actualMessage = null)
        {
            this.ThrowNewHttpBadRequestResultAssertionException(
                "with",
                expectedMessage == null ? "error message" : $"{expectedMessage}",
                $"instead received {(actualMessage == null ? "non-string value" : $"{actualMessage}")}");
        }

        private void ThrowNewHttpBadRequestResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new BadRequestResultAssertionException(string.Format(
                "When calling {0} action in {1} expected bad request result {2} {3}, but {4}.",
                this.ActionName,
                this.Controller.GetName(),
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
