namespace MyTested.AspNetCore.Mvc.Builders.Http
{
    using System;
    using System.Collections.Generic;
    using Contracts.Http;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Http.HttpResponse"/> cookie.
    /// </summary>
    public class ResponseCookieTestBuilder : IAndResponseCookieTestBuilder
    {
        private const string FakeCookieName = "__cookie__";

        private readonly SetCookieHeaderValue responseCookie;
        private readonly ICollection<Func<SetCookieHeaderValue, SetCookieHeaderValue, bool>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCookieTestBuilder"/> class.
        /// </summary>
        public ResponseCookieTestBuilder()
        {
            this.validations = new List<Func<SetCookieHeaderValue, SetCookieHeaderValue, bool>>();
            this.responseCookie = new SetCookieHeaderValue(FakeCookieName);
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithName(string name)
        {
            this.responseCookie.Name = name;
            this.validations.Add((expected, actual) => expected.Name == actual.Name);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithValue(string value)
        {
            this.responseCookie.Value = value;
            this.validations.Add((expected, actual) => expected.Value == actual.Value);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithValue(Action<string> assertions)
        {
            this.validations.Add((expected, actual) =>
            {
                assertions(actual.Value.Value);
                return true;
            });

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithValue(Func<string, bool> predicate)
        {
            this.validations.Add((expected, actual) => predicate(actual.Value.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithDomain(string domain)
        {
            this.responseCookie.Domain = domain;
            this.validations.Add((expected, actual) => expected.Domain == actual.Domain);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithExpiration(DateTimeOffset? expiration)
        {
            this.responseCookie.Expires = expiration;
            this.validations.Add((expected, actual) => expected.Expires == actual.Expires);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithHttpOnly(bool httpOnly)
        {
            this.responseCookie.HttpOnly = httpOnly;
            this.validations.Add((expected, actual) => expected.HttpOnly == actual.HttpOnly);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithMaxAge(TimeSpan? maxAge)
        {
            this.responseCookie.MaxAge = maxAge;
            this.validations.Add((expected, actual) => expected.MaxAge == actual.MaxAge);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithPath(string path)
        {
            this.responseCookie.Path = path;
            this.validations.Add((expected, actual) => expected.Path == actual.Path);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithSameSite(SameSiteMode sameSite)
        {
            this.responseCookie.SameSite = sameSite;
            this.validations.Add((expected, actual) => expected.SameSite == actual.SameSite);
            return this;
        }

        /// <inheritdoc />
        public IAndResponseCookieTestBuilder WithSecurity(bool security)
        {
            this.responseCookie.Secure = security;
            this.validations.Add((expected, actual) => expected.Secure == actual.Secure);
            return this;
        }

        /// <inheritdoc />
        public IResponseCookieTestBuilder AndAlso() => this;

        internal SetCookieHeaderValue GetResponseCookie()
        {
            if (this.responseCookie.Name == FakeCookieName)
            {
                throw new InvalidOperationException("Cookie name must be provided. 'WithName' method must be called on the cookie builder in order to run this test case successfully.");
            }

            return this.responseCookie;
        }

        internal ICollection<Func<SetCookieHeaderValue, SetCookieHeaderValue, bool>> GetResponseCookieValidations() 
            => this.validations;
    }
}
