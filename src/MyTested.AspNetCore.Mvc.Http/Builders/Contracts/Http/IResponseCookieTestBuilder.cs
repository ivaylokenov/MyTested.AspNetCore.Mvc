namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Http
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Http.HttpResponse"/> cookie.
    /// </summary>
    public interface IResponseCookieTestBuilder
    {
        /// <summary>
        /// Sets the expected <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Name"/> of the tested cookie.
        /// </summary>
        /// <param name="name">Name to set on the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithName(string name);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Value"/> property is the same as the provided one.
        /// </summary>
        /// <param name="value">Expected value of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithValue(string value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Value"/> passes the provided assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the cookie value.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithValue(Action<string> assertions);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Value"/> passes the provided predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the cookie value.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithValue(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Domain"/> property is the same as provided one.
        /// </summary>
        /// <param name="domain">Expected domain of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithDomain(string domain);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Expires"/> property is the same as provided one.
        /// </summary>
        /// <param name="expires">Expected expiration date time offset of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithExpired(DateTimeOffset? expires);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.HttpOnly"/> property is the same as provided one.
        /// </summary>
        /// <param name="httpOnly">Expected <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.HttpOnly"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithHttpOnly(bool httpOnly);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.MaxAge"/> property is the same as provided one.
        /// </summary>
        /// <param name="maxAge">Expected <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.MaxAge"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithMaxAge(TimeSpan? maxAge);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Path"/> property is the same as provided one.
        /// </summary>
        /// <param name="path">Expected <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Path"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithPath(string path);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Secure"/> property is the same as provided one.
        /// </summary>
        /// <param name="secure">Expected <see cref="Microsoft.Net.Http.Headers.SetCookieHeaderValue.Secure"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithSecure(bool secure);
    }
}
