namespace MyTested.Mvc.Builders.Contracts.Http
{
    /// <summary>
    /// Used for adding AndAlso() method to the the HTTP response message tests.
    /// </summary>
    public interface IAndHttpResponseTestBuilder : IHttpResponseTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IHttpResponseTestBuilder AndAlso();
    }
}
