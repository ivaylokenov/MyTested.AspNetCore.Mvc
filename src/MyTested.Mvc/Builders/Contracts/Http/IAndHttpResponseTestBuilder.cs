namespace MyTested.Mvc.Builders.Contracts.Http
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Http.HttpResponse"/> tests.
    /// </summary>
    public interface IAndHttpResponseTestBuilder : IHttpResponseTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Http.HttpResponse"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IHttpResponseTestBuilder"/>.</returns>
        IHttpResponseTestBuilder AndAlso();
    }
}
