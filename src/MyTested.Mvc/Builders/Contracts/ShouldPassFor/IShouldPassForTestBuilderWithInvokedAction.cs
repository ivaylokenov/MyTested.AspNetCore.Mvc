namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public interface IShouldPassForTestBuilderWithInvokedAction : IShouldPassForTestBuilderWithAction
    {
        IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Action<Exception> assertions);

        IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Func<Exception, bool> predicate);

        IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Action<HttpResponse> assertions);

        IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Func<HttpResponse, bool> predicate);
    }
}
