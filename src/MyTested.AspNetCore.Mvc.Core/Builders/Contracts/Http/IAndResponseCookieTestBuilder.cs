namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Http
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Http.HttpResponse.Cookies"/> tests.
    /// </summary>
    public interface IAndResponseCookieTestBuilder : IResponseCookieTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Http.HttpResponse.Cookies"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IResponseCookieTestBuilder"/>.</returns>
        IResponseCookieTestBuilder AndAlso();
    }
}
