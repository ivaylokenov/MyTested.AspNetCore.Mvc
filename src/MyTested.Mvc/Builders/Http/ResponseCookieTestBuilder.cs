namespace MyTested.Mvc.Builders.Http
{
    using System;
    using System.Collections.Generic;
    using Contracts.Http;
    using Microsoft.Net.Http.Headers;

    public class ResponseCookieTestBuilder : IAndResponseCookieTestBuilder
    {
        private const string FakeCookieName = "__cookie__";

        private readonly SetCookieHeaderValue responseCookie;
        private readonly ICollection<Func<SetCookieHeaderValue, SetCookieHeaderValue, bool>> validations;

        public ResponseCookieTestBuilder()
        {
            this.validations = new List<Func<SetCookieHeaderValue, SetCookieHeaderValue, bool>>();
            this.responseCookie = new SetCookieHeaderValue(FakeCookieName);
        }

        public IAndResponseCookieTestBuilder WithName(string name)
        {
            this.responseCookie.Name = name;
            this.validations.Add((expected, actual) => expected.Name == actual.Name);
            return this;
        }

        public IAndResponseCookieTestBuilder WithValue(string value)
        {
            this.responseCookie.Value = value;
            this.validations.Add((expected, actual) => expected.Value == actual.Value);
            return this;
        }

        public IAndResponseCookieTestBuilder WithValue(Action<string> assertions)
        {
            this.validations.Add((expected, actual) =>
            {
                assertions(actual.Value);
                return true;
            });

            return this;
        }

        public IAndResponseCookieTestBuilder WithValue(Func<string, bool> predicate)
        {
            this.validations.Add((expected, actual) => predicate(actual.Value));
            return this;
        }

        public IAndResponseCookieTestBuilder WithDomain(string domain)
        {
            this.responseCookie.Domain = domain;
            this.validations.Add((expected, actual) => expected.Domain == actual.Domain);
            return this;
        }

        public IAndResponseCookieTestBuilder WithExpired(DateTimeOffset? expires)
        {
            this.responseCookie.Expires = expires;
            this.validations.Add((expected, actual) => expected.Expires == actual.Expires);
            return this;
        }

        public IAndResponseCookieTestBuilder WithHttpOnly(bool httpOnly)
        {
            this.responseCookie.HttpOnly = httpOnly;
            this.validations.Add((expected, actual) => expected.HttpOnly == actual.HttpOnly);
            return this;
        }

        public IAndResponseCookieTestBuilder WithMaxAge(TimeSpan? maxAge)
        {
            this.responseCookie.MaxAge = maxAge;
            this.validations.Add((expected, actual) => expected.MaxAge == actual.MaxAge);
            return this;
        }

        public IAndResponseCookieTestBuilder WithPath(string path)
        {
            this.responseCookie.Path = path;
            this.validations.Add((expected, actual) => expected.Path == actual.Path);
            return this;
        }

        public IAndResponseCookieTestBuilder WithSecure(bool secure)
        {
            this.responseCookie.Secure = secure;
            this.validations.Add((expected, actual) => expected.Secure == actual.Secure);
            return this;
        }

        internal SetCookieHeaderValue GetResponseCookie()
        {
            if (this.responseCookie.Name == FakeCookieName)
            {
                throw new InvalidOperationException("Cookie name must be provided. 'WithName' method must be called on the cookie builder in order to run this test case successfully.");
            }

            return this.responseCookie;
        }

        internal ICollection<Func<SetCookieHeaderValue, SetCookieHeaderValue, bool>> GetResponseCookieValidations()
        {
            return this.validations;
        }

        public IResponseCookieTestBuilder AndAlso()
        {
            return this;
        }
    }
}
