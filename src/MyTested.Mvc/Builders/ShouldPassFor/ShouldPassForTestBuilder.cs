namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class ShouldPassForTestBuilder : IShouldPassForTestBuilder
    {
        private readonly HttpTestContext testContext;

        public ShouldPassForTestBuilder(HttpTestContext testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            this.testContext = testContext;
        }

        public IShouldPassForTestBuilder TheHttpContext(Action<HttpContext> assertions)
        {
            this.ValidateFor(assertions, this.testContext.HttpContext);
            return this;
        }

        public IShouldPassForTestBuilder TheHttpContext(Func<HttpContext, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.HttpContext);
            return this;
        }

        public IShouldPassForTestBuilder TheHttpRequest(Action<HttpRequest> assertions)
        {
            this.ValidateFor(assertions, this.testContext.HttpRequest);
            return this;
        }

        public IShouldPassForTestBuilder TheHttpRequest(Func<HttpRequest, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.HttpRequest);
            return this;
        }

        protected void ValidateFor<T>(Action<T> assertions, T obj)
        {
            assertions(obj);
        }

        protected void ValidateFor<T>(Func<T, bool> predicate, T obj)
        {
            if (!predicate(obj))
            {
                throw new InvalidAssertionException($"Expected the {obj.GetName()} to pass the given predicate but it failed.");
            }
        }
    }
}
