namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    using System.Net;
    using Base;
    using HttpResponseMessages;

    // TODO: this may not be needed
    /// <summary>
    /// Used for testing expected HttpResponseException.
    /// </summary>
    public interface IHttpResponseExceptionTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether caught HttpResponseException has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Provides methods to test whether caught HttpResponseException has specific HttpResponseMessage.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpResponseMessageTestBuilder WithHttpResponseMessage();
    }
}
