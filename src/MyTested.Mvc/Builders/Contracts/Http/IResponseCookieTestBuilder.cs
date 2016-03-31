namespace MyTested.Mvc.Builders.Contracts.Http
{
    using System;

    public interface IResponseCookieTestBuilder
    {
        IAndResponseCookieTestBuilder WithName(string name);

        IAndResponseCookieTestBuilder WithValue(string value);

        IAndResponseCookieTestBuilder WithValue(Action<string> assertions);
        
        IAndResponseCookieTestBuilder WithValue(Func<string, bool> predicate);

        IAndResponseCookieTestBuilder WithDomain(string domain);

        IAndResponseCookieTestBuilder WithExpired(DateTimeOffset? expires);

        IAndResponseCookieTestBuilder WithHttpOnly(bool httpOnly);

        IAndResponseCookieTestBuilder WithMaxAge(TimeSpan? maxAge);

        IAndResponseCookieTestBuilder WithPath(string path);

        IAndResponseCookieTestBuilder WithSecure(bool secure);
    }
}
