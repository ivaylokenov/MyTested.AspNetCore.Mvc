namespace MyTested.Mvc.Builders.Contracts.ShouldPass
{
    using System;
    using Microsoft.AspNetCore.Http;

    public interface IShouldPassForTestBuilder
    {
        IShouldPassForTestBuilder TheHttpContext(Action<HttpContext> assertions);

        IShouldPassForTestBuilder TheHttpContext(Func<HttpContext, bool> assertions);

        IShouldPassForTestBuilder TheHttpRequest(Action<HttpRequest> assertions);

        IShouldPassForTestBuilder TheHttpRequest(Func<HttpRequest, bool> assertions);
    }
}
