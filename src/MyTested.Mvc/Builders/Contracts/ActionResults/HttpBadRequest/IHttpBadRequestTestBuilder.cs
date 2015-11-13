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
        // TODO: check if applicable
        ///// <summary>
        ///// Tests HTTP bad request result with specific error using test builder.
        ///// </summary>
        ///// <returns>Bad request with error message test builder.</returns>
        //IBadRequestErrorMessageTestBuilder WithErrorMessage();

        // TODO: error is now object
        /// <summary>
        /// Tests bad request result with specific error message provided by string.
        /// </summary>
        /// <param name="message">Expected error message from bad request result.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException WithErrorMessage(string message);

        /// <summary>
        /// Tests bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException WithModelState(ModelStateDictionary modelState);

        /// <summary>
        /// Tests bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>Model error test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> WithModelStateFor<TRequestModel>();
    }
}
