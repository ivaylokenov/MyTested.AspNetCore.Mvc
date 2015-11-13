namespace MyTested.Mvc.Builders.Contracts.HttpResponseMessages
{
    /// <summary>
    /// Used for adding AndAlso() method to the the HTTP response message tests.
    /// </summary>
    public interface IAndHttpResponseMessageTestBuilder : IHttpResponseMessageTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IHttpResponseMessageTestBuilder AndAlso();
    }
}
