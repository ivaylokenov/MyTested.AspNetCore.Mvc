namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.BadRequest
{
    using Contracts.Base;

    /// <summary>
    /// Used for testing specific <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/> text error messages.
    /// </summary>
    public interface IBadRequestErrorMessageTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>Test builder of type <see cref="IAndBadRequestTestBuilder"/>.</returns>
        IAndBadRequestTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>Test builder of type <see cref="IAndBadRequestTestBuilder"/>.</returns>
        IAndBadRequestTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>Test builder of type <see cref="IAndBadRequestTestBuilder"/>.</returns>
        IAndBadRequestTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>Test builder of type <see cref="IAndBadRequestTestBuilder"/>.</returns>
        IAndBadRequestTestBuilder Containing(string containsMessage);
    }
}
