namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using Microsoft.AspNetCore.Http;

    public interface IShouldPassForTestBuilder
    {
        IShouldPassForTestBuilder TheHttpContext(Action<HttpContext> assertions);

        IShouldPassForTestBuilder TheHttpContext(Func<HttpContext, bool> predicate);

        IShouldPassForTestBuilder TheHttpRequest(Action<HttpRequest> assertions);

        IShouldPassForTestBuilder TheHttpRequest(Func<HttpRequest, bool> predicate);
    }
}
