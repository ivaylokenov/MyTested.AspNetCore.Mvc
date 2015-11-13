namespace MyTested.Mvc.Builders.ActionResults.HttpBadRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.ActionResults.HttpBadRequest;
    using Contracts.Models;
    using Models;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ModelBinding;
    using Exceptions;

    /// <summary>
    /// Used for testing HTTP bad request results.
    /// </summary>
    /// <typeparam name="TBadRequestResult">Type of bad request result - BadRequestResult or BadRequestObjectResult.</typeparam>
    public class HttpBadRequestTestBuilder<TBadRequestResult> : BaseTestBuilderWithActionResult<TBadRequestResult>,
        IHttpBadRequestTestBuilder
    {
        private const string ErrorMessage = "error message";
        private const string ModelStateDictionary = "model state dictionary";

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
        }

        // TODO: check if applicable?
        ///// <summary>
        ///// Tests bad request result with specific error message using test builder.
        ///// </summary>
        ///// <returns>Bad request with error message test builder.</returns>
        //public IBadRequestErrorMessageTestBuilder WithErrorMessage()
        //{
        //    var badRequestErrorMessageResult = this.GetBadRequestResult<BadRequestErrorMessageResult>(ErrorMessage);
        //    return new BadRequestErrorMessageTestBuilder(
        //        this.Controller,
        //        this.ActionName,
        //        this.CaughtException,
        //        badRequestErrorMessageResult.Message);
        //}

        /// <summary>
        /// Tests bad request result with specific error message provided by string.
        /// </summary>
        /// <param name="message">Expected error message from bad request result.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException WithErrorMessage(string message)
        {

            // TODO: serializable error
            var badRequestErrorMessageResult = this.GetBadRequestResult<BadRequestObjectResult>(ErrorMessage);
            //var actualMessage = badRequestErrorMessageResult.Message;
            //this.ValidateErrorMessage(message, actualMessage);

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException WithModelState(ModelStateDictionary modelState)
        {
            // TODO: serializable error
            //var invalidModelStateResult = this.GetBadRequestResult<BadRequestObjectResult>(ModelStateDictionary);
            //var actualModelState = invalidModelStateResult.ModelState;

            //var expectedKeysCount = modelState.Keys.Count;
            //var actualKeysCount = actualModelState.Keys.Count;

            //if (expectedKeysCount != actualKeysCount)
            //{
            //    throw new BadRequestResultAssertionException(string.Format(
            //            "When calling {0} action in {1} expected bad request model state dictionary to contain {2} keys, but found {3}.",
            //            this.ActionName,
            //            this.Controller.GetName(),
            //            expectedKeysCount,
            //            actualKeysCount));
            //}

            //var actualModelStateSortedKeys = actualModelState.Keys.OrderBy(k => k).ToList();
            //var expectedModelStateSortedKeys = modelState.Keys.OrderBy(k => k).ToList();

            //foreach (var expectedKey in expectedModelStateSortedKeys)
            //{
            //    if (!actualModelState.ContainsKey(expectedKey))
            //    {
            //        throw new BadRequestResultAssertionException(string.Format(
            //            "When calling {0} action in {1} expected bad request model state dictionary to contain {2} key, but none found.",
            //            this.ActionName,
            //            this.Controller.GetName(),
            //            expectedKey));
            //    }

            //    var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
            //    var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

            //    if (expectedSortedErrors.Count != actualSortedErrors.Count)
            //    {
            //        throw new BadRequestResultAssertionException(string.Format(
            //            "When calling {0} action in {1} expected bad request model state dictionary to contain {2} errors for {3} key, but found {4}.",
            //            this.ActionName,
            //            this.Controller.GetName(),
            //            expectedSortedErrors.Count,
            //            expectedKey,
            //            actualSortedErrors.Count));
            //    }

            //    for (int i = 0; i < expectedSortedErrors.Count; i++)
            //    {
            //        var expectedError = expectedSortedErrors[i];
            //        var actualError = actualSortedErrors[i];
            //        this.ValidateErrorMessage(expectedError, actualError);
            //    }
            //}

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>Model error test builder.</returns>
        public IModelErrorTestBuilder<TRequestModel> WithModelStateFor<TRequestModel>()
        {
            var invalidModelStateResult = this.GetBadRequestResult<BadRequestObjectResult>(ModelStateDictionary);
            return new ModelErrorTestBuilder<TRequestModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException
                /*modelState: invalidModelStateResult.ModelState*/); // TODO:: serializable error
        }

        private static IList<string> GetSortedErrorMessagesForModelStateKey(IEnumerable<ModelError> errors)
        {
            return errors
                .OrderBy(er => er.ErrorMessage)
                .Select(er => er.ErrorMessage)
                .ToList();
        }

        private TExpectedBadRequestResult GetBadRequestResult<TExpectedBadRequestResult>(string containment)
            where TExpectedBadRequestResult : class
        {
            var actualBadRequestResult = this.ActionResult as TExpectedBadRequestResult;
            if (actualBadRequestResult == null)
            {
                throw new HttpBadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request result to contain {2}, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    containment));
            }

            return actualBadRequestResult;
        }

        private void ValidateErrorMessage(string expectedMessage, string actualMessage)
        {
            if (expectedMessage != actualMessage)
            {
                throw new HttpBadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request with message '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedMessage,
                    actualMessage));
            }
        }
    }
}
