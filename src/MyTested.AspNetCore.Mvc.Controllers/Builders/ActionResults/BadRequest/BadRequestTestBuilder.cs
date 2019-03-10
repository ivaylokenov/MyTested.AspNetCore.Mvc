namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.BadRequest
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Contracts.ActionResults.BadRequest;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing bad request results.
    /// </summary>
    /// <typeparam name="THttpBadRequestResult">Type of bad request result - <see cref="BadRequestResult"/> or <see cref="BadRequestObjectResult"/>.</typeparam>
    public class BadRequestTestBuilder<THttpBadRequestResult>
        : BaseTestBuilderWithResponseModel<THttpBadRequestResult>, IAndBadRequestTestBuilder
        where THttpBadRequestResult : ActionResult
    {
        private const string ErrorMessage = "{0} bad request result error to be the given object, but in fact it was a different.";
        private const string OfTypeErrorMessage = "{0} bad request result error to be of {1} type, but instead received {2}.";

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
                    "{0} bad request error message ('{1}') to pass the given predicate, but it failed.",
                    this.TestContext.ExceptionMessagePrefix,
                    actualErrorMessage));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder WithNoError()
        {
            var actualResult = this.TestContext.MethodResult as BadRequestResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "{0} bad request result to not have error message, but in fact such was found.",
                    this.TestContext.ExceptionMessagePrefix));
            }

            return this;
        }

        /// <inheritdoc />
        public IBadRequestTestBuilder AndAlso() => this;

        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewHttpBadRequestResultAssertionException(propertyName, expectedValue, actualValue);

        public object GetBadRequestObjectResultValue()
        {
            var actualBadRequestResult = this.TestContext.MethodResult as BadRequestObjectResult;
            if (actualBadRequestResult == null)
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "{0} bad request result to contain error object, but it could not be found.",
                    this.TestContext.ExceptionMessagePrefix));
            }

            return actualBadRequestResult.Value;
        }

        public string GetBadRequestErrorMessage()
        {
            var errorMessage = this.GetBadRequestObjectResultValue() as string;
            if (errorMessage == null)
            {
                this.ThrowNewHttpBadRequestResultAssertionExceptionWithMessage();
            }

            return errorMessage;
        }

        public void ValidateErrorMessage(string expectedMessage, string actualMessage)
        {
            if (expectedMessage != actualMessage)
            {
                this.ThrowNewHttpBadRequestResultAssertionExceptionWithMessage($"message '{expectedMessage}'", $"'{actualMessage}'");
            }
        }

        public ModelStateDictionary GetModelStateFromSerializableError(object error)
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
                "{0} bad request result {1} {2}, but {3}.",
                this.TestContext.ExceptionMessagePrefix,
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
