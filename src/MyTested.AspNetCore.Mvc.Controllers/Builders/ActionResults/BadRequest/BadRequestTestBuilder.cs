namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.BadRequest
{
    using Base;
    using Contracts.ActionResults.BadRequest;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing bad request results.
    /// </summary>
    /// <typeparam name="TBadRequestResult">
    /// Type of bad request result - <see cref="BadRequestResult"/>
    /// or <see cref="BadRequestObjectResult"/>.
    /// </typeparam>
    public class BadRequestTestBuilder<TBadRequestResult> 
        : BaseTestBuilderWithResponseModel<TBadRequestResult>,
        IAndBadRequestTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndBadRequestTestBuilder> 
        where TBadRequestResult : ActionResult
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

        /// <summary>
        /// Gets the bad request result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndBadRequestTestBuilder"/> type.</value>
        public IAndBadRequestTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public IBadRequestTestBuilder AndAlso() => this;
        
        public object GetBadRequestObjectResultValue()
        {
            var actualBadRequestResult = this.TestContext.MethodResult as BadRequestObjectResult;
            if (actualBadRequestResult == null)
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "{0} bad request result to contain error object, but such could not be found.",
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
                if (errorKeyValuePair.Value is string[] errorValues)
                {
                    errorValues.ForEach(er => result.AddModelError(errorKey, er));
                }
            }

            return result;
        }

        /// <summary>
        /// Throws new <see cref="BadRequestResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new BadRequestResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "bad request",
                propertyName,
                expectedValue,
                actualValue));

        private void ThrowNewHttpBadRequestResultAssertionExceptionWithMessage(string expectedMessage = null, string actualMessage = null) 
            => this.ThrowNewFailedValidationException(
                "with",
                expectedMessage == null ? "error message" : $"{expectedMessage}",
                $"instead received {(actualMessage == null ? "non-string value" : $"{actualMessage}")}");
    }
}
