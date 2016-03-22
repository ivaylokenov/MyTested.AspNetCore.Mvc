namespace MyTested.Mvc.Builders.ActionResults.BadRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.ActionResults.BadRequest;
    using Contracts.Models;
    using Exceptions;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Models;
    using System.Net;
    using Microsoft.Net.Http.Headers;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Internal.TestContexts;
    using System;

    /// <summary>
    /// Used for testing bad request results.
    /// </summary>
    /// <typeparam name="THttpBadRequestResult">Type of bad request result - BadRequestResult or BadRequestObjectResult.</typeparam>
    public class BadRequestTestBuilder<THttpBadRequestResult>
        : BaseTestBuilderWithResponseModel<THttpBadRequestResult>, IAndBadRequestTestBuilder
        where THttpBadRequestResult : ActionResult
    {
        private const string ErrorMessage = "When calling {0} action in {1} expected bad request result error to be the given object, but in fact it was a different.";
        private const string OfTypeErrorMessage = "When calling {0} action in {1} expected bad request result error to be of {2} type, but instead received {3}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpBadRequestTestBuilder{TBadRequestResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="httpBadRequestResult">Result from the tested action.</param>
        public BadRequestTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.ErrorMessageFormat = ErrorMessage;
            this.OfTypeErrorMessageFormat = OfTypeErrorMessage;
        }

        /// <summary>
        /// Tests whether bad request result contains deeply equal error value as the provided error object.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <param name="error">Error object.</param>
        /// <returns>Model details test builder.</returns>
        public IModelDetailsTestBuilder<TError> WithError<TError>(TError error)
        {
            return this.WithResponseModel(error);
        }

        /// <summary>
        /// Tests whether bad request result contains error object of the provided type.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <returns>Model details test builder.</returns>
        public IModelDetailsTestBuilder<TError> WithErrorOfType<TError>()
        {
            return this.WithResponseModelOfType<TError>();
        }

        /// <summary>
        /// Tests whether no specific error is returned from the bad request result.
        /// </summary>
        /// <returns>The same bad request test builder.</returns>
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

        /// <summary>
        /// Tests whether bad request result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder WithStatusCode(int statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the content type provided as MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the same content types provided as enumerable of MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the same content types provided as MediaTypeHeaderValue parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of IOutputFormatter.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of IOutputFormatter.</typeparam>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of IOutputFormatter.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <summary>
        /// Tests whether bad request result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Output formatter parameters.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <summary>
        /// Tests bad request result with specific text error using test builder.
        /// </summary>
        /// <returns>Bad request with error message test builder.</returns>
        public IBadRequestErrorMessageTestBuilder WithErrorMessage()
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            return new BadRequestErrorMessageTestBuilder(
                this.TestContext,
                actualErrorMessage,
                this);
        }

        /// <summary>
        /// Tests bad request result with specific text error message provided by string.
        /// </summary>
        /// <param name="error">Expected text error message from bad request result.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder WithErrorMessage(string error)
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            this.ValidateErrorMessage(error, actualErrorMessage);

            return this;
        }

        /// <summary>
        /// Tests whether bad request result error message passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the error message.</param>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder WithErrorMessage(Action<string> assertions)
        {
            var actualErrorMessage = this.GetBadRequestErrorMessage();
            assertions(actualErrorMessage);

            return this;
        }

        /// <summary>
        /// Tests bad request result error message whether it passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the error message.</param>
        /// <returns>The same bad request test builder.</returns>
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

        /// <summary>
        /// Tests whether bad request result contains the controller's ModelState dictionary as object error.
        /// </summary>
        /// <returns>The same bad request test builder.</returns>
        public IAndBadRequestTestBuilder WithModelStateError()
        {
            return this.WithModelStateError(this.TestContext.ControllerContext.ModelState);
        }

        /// <summary>
        /// Tests bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>The same bad request test builder.</returns>
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

        /// <summary>
        /// Tests bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>Model error test builder.</returns>
        public IModelErrorTestBuilder<TRequestModel> WithModelStateErrorFor<TRequestModel>()
        {
            this.TestContext.Model = this.GetBadRequestObjectResultValue();
            return new ModelErrorTestBuilder<TRequestModel>(
                this.TestContext,
                modelState: this.GetModelStateFromSerializableError(this.TestContext.Model));
        }

        /// <summary>
        /// AndAlso method for better readability when chaining bad request result tests.
        /// </summary>
        /// <returns>HTTP bad request result test builder.</returns>
        public IBadRequestTestBuilder AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <returns>Action result to be tested.</returns>
        public new ActionResult AndProvideTheActionResult() => this.ActionResult;

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
