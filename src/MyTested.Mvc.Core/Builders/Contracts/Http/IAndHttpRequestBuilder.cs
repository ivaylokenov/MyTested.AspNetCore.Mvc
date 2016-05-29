namespace MyTested.Mvc.Builders.Contracts.Http
{
    /// <summary>
    /// Used for adding AndAlso() method to <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> builder.
    /// </summary>
    public interface IAndHttpRequestBuilder : IHttpRequestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.AspNetCore.Http.HttpRequest"/>.
        /// </summary>
        /// <returns>The same <see cref="IHttpRequestBuilder"/>.</returns>
        IHttpRequestBuilder AndAlso();
    }
}
