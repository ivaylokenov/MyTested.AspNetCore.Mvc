namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Utilities.Extensions;
    public class ShouldPassForTestBuilderWithInvokedAction : ShouldPassForTestBuilderWithAction,
        IShouldPassForTestBuilderWithInvokedAction
    {
        public ShouldPassForTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Action<Exception> assertions)
        {
            assertions(this.TestContext.CaughtException);
            return this;
        }

        public IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Func<Exception, bool> predicate)
        {
            var exception = this.TestContext.CaughtException;
            this.ValidateFor(predicate, exception, exception != null ? exception.GetName() : "caught exception");
            return this;
        }

        public IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Action<HttpResponse> assertions)
        {
            assertions(this.TestContext.HttpResponse);
            return this;
        }

        public IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Func<HttpResponse, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.HttpResponse, nameof(HttpResponse));
            return this;
        }
    }
}
