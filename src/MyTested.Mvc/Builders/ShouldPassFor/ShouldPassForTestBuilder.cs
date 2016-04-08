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
            assertions(this.testContext.HttpContext);
            return this;
        }

        public IShouldPassForTestBuilder TheHttpContext(Func<HttpContext, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.HttpContext, nameof(HttpContext));
            return this;
        }

        public IShouldPassForTestBuilder TheHttpRequest(Action<HttpRequest> assertions)
        {
            assertions(this.testContext.HttpRequest);
            return this;
        }

        public IShouldPassForTestBuilder TheHttpRequest(Func<HttpRequest, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.HttpRequest, nameof(HttpRequest));
            return this;
        }
        
        protected void ValidateFor<T>(Func<T, bool> predicate, T obj, string objectName = null)
        {
            if (!predicate(obj))
            {
                throw new InvalidAssertionException($"Expected the {objectName ?? obj.GetName()} to pass the given predicate but it failed.");
            }
        }
    }
}
