namespace MyTested.Mvc.Builders.Contracts.ActionResults.HttpBadRequest
{
    using Base;

    /// <summary>
    /// Used for testing specific bad request text error messages.
    /// </summary>
    public interface IHttpBadRequestErrorMessageTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>HTTP bad request test builder.</returns>
        IAndHttpBadRequestTestBuilder Containing(string containsMessage);
    }
}
