namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.BadRequest
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
    /// and <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
    /// </summary>
    public interface IBadRequestTestBuilder : IBaseTestBuilderWithOutputResult<IAndBadRequestTestBuilder>
    {
        /// <summary>
        /// Tests whether no specific error is returned
        /// from the <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        IAndBadRequestTestBuilder WithNoError();

        /// <summary>
        /// Tests <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/> with
        /// specific text error message using test builder.
        /// </summary>
        /// <returns><see cref="IBadRequestErrorMessageTestBuilder"/>.</returns>
        IBadRequestErrorMessageTestBuilder WithErrorMessage();

        /// <summary>
        /// Tests <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/> with
        /// specific text error message provided as string.
        /// </summary>
        /// <param name="message">Expected error message.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        IAndBadRequestTestBuilder WithErrorMessage(string message);
    }
}
