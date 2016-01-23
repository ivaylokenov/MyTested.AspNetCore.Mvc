namespace MyTested.Mvc.Builders.Contracts.ActionResults.HttpBadRequest
{
    using Base;
    using Microsoft.AspNet.Mvc.ModelBinding;
    using Models;

    /// <summary>
    /// Used for testing HTTP bad request results.
    /// </summary>
    public interface IHttpBadRequestTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether HTTP bad request result contains deeply equal error value as the provided error object.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <param name="error">Error object.</param>
        /// <returns>Model details test builder.</returns>
        IModelDetailsTestBuilder<TError> WithError<TError>(TError error);

        /// <summary>
        /// Tests whether HTTP bad request result contains error object of the provided type.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <returns>Model details test builder.</returns>
        IModelDetailsTestBuilder<TError> WithErrorOfType<TError>();

        /// <summary>
        /// Tests whether no specific error is returned from the HTTP bad request result.
        /// </summary>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithNoError();

        /// <summary>
        /// Tests HTTP bad request result with specific text error message using test builder.
        /// </summary>
        /// <returns>Bad request with error message test builder.</returns>
        IHttpBadRequestErrorMessageTestBuilder WithErrorMessage();

        /// <summary>
        /// Tests HTTP bad request result with specific text error message provided by string.
        /// </summary>
        /// <param name="message">Expected error message from bad request result.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithErrorMessage(string message);

        /// <summary>
        /// Tests whether HTTP bad request result contains the controller's ModelState dictionary as object error.
        /// </summary>
        /// <returns>Base test builder with caught exception.</returns>
        IAndHttpBadRequestTestBuilder WithModelStateError();

        /// <summary>
        /// Tests HTTP bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>The same HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder WithModelStateError(ModelStateDictionary modelState);

        /// <summary>
        /// Tests HTTP bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>The same HTTP bad request test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> WithModelStateErrorFor<TRequestModel>();
    }
}
