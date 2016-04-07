namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;

    public class ShouldPassForTestBuilderWithInvokedAction : ShouldPassForTestBuilderWithAction,
        IShouldPassForTestBuilderWithInvokedAction
    {
        public ShouldPassForTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Action<Exception> assertions)
        {
            this.ValidateFor(assertions, this.TestContext.CaughtException);
            return this;
        }

        public IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Func<Exception, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.CaughtException);
            return this;
        }

        public IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Action<HttpResponse> assertions)
        {
            this.ValidateFor(assertions, this.TestContext.HttpResponse);
            return this;
        }

        public IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Func<HttpResponse, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.HttpResponse);
            return this;
        }
    }
}
