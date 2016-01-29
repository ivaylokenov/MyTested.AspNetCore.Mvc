namespace MyTested.Mvc.Builders.Contracts.Http
{
    /// <summary>
    /// Used for adding AndAlso() method to HTTP request builder.
    /// </summary>
    public interface IAndHttpRequestBuilder : IHttpRequestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building HTTP request.
        /// </summary>
        /// <returns>The same URI test builder.</returns>
        IHttpRequestBuilder AndAlso();
    }
}
