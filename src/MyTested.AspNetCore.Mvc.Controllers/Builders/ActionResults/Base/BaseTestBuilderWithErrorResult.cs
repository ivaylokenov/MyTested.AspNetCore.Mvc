namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Base
{
    using Builders.Base;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with error response.
    /// </summary>
    /// <typeparam name="TErrorResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public abstract class BaseTestBuilderWithErrorResult<TErrorResult>
        : BaseTestBuilderWithResponseModel<TErrorResult>
        where TErrorResult : ActionResult
    {
        private const string ErrorMessage = "{0} {actionResultName} error to be the given object, but in fact it was a different.";
        private const string OfTypeErrorMessage = "{0} {actionResultName} error to be of {1} type, but instead received {2}.";

        private ControllerTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithErrorResult{TErrorResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        /// <param name="actionResultName">Action result name.</param>
        protected BaseTestBuilderWithErrorResult(
            ControllerTestContext testContext, 
            string actionResultName)
            : base(testContext)
        {
            this.TestContext = testContext;
            
            this.ErrorMessageFormat = ErrorMessage.Replace($"{{{nameof(actionResultName)}}}", actionResultName);
            this.OfTypeErrorMessageFormat = OfTypeErrorMessage.Replace($"{{{nameof(actionResultName)}}}", actionResultName);
        }

        /// <summary>
        /// Gets the currently used <see cref="ControllerTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ControllerTestContext"/>.</value>
        public new ControllerTestContext TestContext
        {
            get => this.testContext;

            private set
            {
                CommonValidator.CheckForNullReference(value.Component, nameof(value.Component));
                this.testContext = value;
            }
        }

        public object GetObjectResultValue()
            => this.GetObjectResult().Value;
        
        public string GetErrorMessage()
        {
            var errorMessage = this.GetObjectResultValue() as string;
            if (errorMessage == null)
            {
                this.ThrowNewErrorMessageException();
            }

            return errorMessage;
        }
        
        public void ValidateErrorMessage(string expectedMessage, string actualMessage)
        {
            if (expectedMessage != actualMessage)
            {
                this.ThrowNewErrorMessageException($"message '{expectedMessage}'", $"'{actualMessage}'");
            }
        }
        
        public ModelStateDictionary GetModelStateFromSerializableError(object error)
        {
            error = error ?? this.GetObjectResultValue();

            var serializableError = error as SerializableError;
            if (serializableError == null)
            {
                this.ThrowNewErrorMessageException("model state dictionary as error", "other type of error");
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

        protected void ThrowNewErrorMessageException(string expectedMessage = null, string actualMessage = null)
            => this.ThrowNewFailedValidationException(
                "with",
                expectedMessage == null ? "error message" : $"{expectedMessage}",
                $"instead received {(actualMessage == null ? "non-string value" : $"{actualMessage}")}");
    }
}
