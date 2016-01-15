namespace MyTested.Mvc.Builders.ActionResults.HttpBadRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.ActionResults.HttpBadRequest;
    using Contracts.Base;
    using Contracts.Models;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ModelBinding;
    using Models;

    /// <summary>
    /// Used for testing HTTP bad request results.
    /// </summary>
    /// <typeparam name="TBadRequestResult">Type of bad request result - BadRequestResult or BadRequestObjectResult.</typeparam>
    public class HttpBadRequestTestBuilder<TBadRequestResult> : BaseResponseModelTestBuilder<TBadRequestResult>,
        IHttpBadRequestTestBuilder
    {
        private const string ErrorMessage = "When calling {0} action in {1} expected HTTP bad request result error to be the given object, but in fact it was a different.";
        private const string OfTypeErrorMessage = "When calling {0} action in {1} expected HTTP bad request result error to be of {2} type, but instead received {3}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpBadRequestTestBuilder{TBadRequestResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public HttpBadRequestTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TBadRequestResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
            this.ErrorMessageFormat = ErrorMessage;
            this.OfTypeErrorMessageFormat = OfTypeErrorMessage;
        }
        
        public IModelDetailsTestBuilder<TError> WithError<TError>(TError error)
        {
            return this.WithResponseModel(error);
        }

        public IModelDetailsTestBuilder<TError> WithErrorOfType<TError>()
        {
            return this.WithResponseModelOfType<TError>();
        }

        /// <summary>
        /// Tests HTTP bad request result with specific error using test builder.
        /// </summary>
        /// <returns>Bad request with error message test builder.</returns>
        public IHttpBadRequestErrorMessageTestBuilder WithErrorMessage()
        {
            var badRequestObjectResultValue = this.GetBadRequestObjectResultValue() as string;
            if (badRequestObjectResultValue == null)
            {
                this.ThrowNewHttpBadRequestResultAssertionException();
            }

            return new HttpBadRequestErrorMessageTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                badRequestObjectResultValue);
        }

        /// <summary>
        /// Tests HTTP bad request result with specific text error message provided by string.
        /// </summary>
        /// <param name="error">Expected text error message from bad request result.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException WithErrorMessage(string error)
        {
            var badRequestObjectResultValue = this.GetBadRequestObjectResultValue();
            var actualMessage = badRequestObjectResultValue as string;
            this.ValidateErrorMessage(error, actualMessage);

            return this.NewAndProvideTestBuilder();
        }
        
        public IBaseTestBuilderWithCaughtException WithModelState()
        {
            return this.WithModelState(this.Controller.ModelState);
        }

        /// <summary>
        /// Tests HTTP bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException WithModelState(ModelStateDictionary modelState)
        {
            var badRequestObjectResultValue = this.GetBadRequestObjectResultValue();
            var actualModelState = this.GetModelStateFromSerializableError(badRequestObjectResultValue);

            var expectedKeysCount = modelState.Keys.Count;
            var actualKeysCount = actualModelState.Keys.Count;

            if (expectedKeysCount != actualKeysCount)
            {
                throw new HttpBadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected HTTP bad request model state dictionary to contain {2} keys, but found {3}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedKeysCount,
                        actualKeysCount));
            }

            var actualModelStateSortedKeys = actualModelState.Keys.OrderBy(k => k).ToList();
            var expectedModelStateSortedKeys = modelState.Keys.OrderBy(k => k).ToList();

            foreach (var expectedKey in expectedModelStateSortedKeys)
            {
                if (!actualModelState.ContainsKey(expectedKey))
                {
                    throw new HttpBadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected HTTP bad request model state dictionary to contain {2} key, but none found.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedKey));
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    throw new HttpBadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected HTTP bad request model state dictionary to contain {2} errors for {3} key, but found {4}.",
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

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests HTTP bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>Model error test builder.</returns>
        public IModelErrorTestBuilder<TRequestModel> WithModelStateFor<TRequestModel>()
        {
            var badRequestObjectResultValue = this.GetBadRequestObjectResultValue();
            return new ModelErrorTestBuilder<TRequestModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                modelState: this.GetModelStateFromSerializableError(badRequestObjectResultValue));
        }

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
                throw new HttpBadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP bad request result to contain error object, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return actualBadRequestResult.Value;
        }

        private void ValidateErrorMessage(string expectedMessage, string actualMessage)
        {
            if (expectedMessage != actualMessage)
            {
                this.ThrowNewHttpBadRequestResultAssertionException(expectedMessage, actualMessage);
            }
        }

        private ModelStateDictionary GetModelStateFromSerializableError(object error)
        {
            var serializableError = error as SerializableError;
            if (serializableError == null)
            {
                this.ThrowNewHttpBadRequestResultAssertionException("model state dictionary", "other type of error");
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

        private void ThrowNewHttpBadRequestResultAssertionException(string expectedMessage = null, string actualMessage = null)
        {
            throw new HttpBadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP bad request with {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedMessage == null ? "error message" : $"message '{expectedMessage}'",
                    actualMessage == null ? "non-string value" : $"'{actualMessage}'"));
        }
    }
}
