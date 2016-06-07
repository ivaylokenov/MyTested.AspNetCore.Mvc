namespace MyTested.AspNetCore.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    public class ShouldPassForTestBuilder : IShouldPassForTestBuilder
    {
        private readonly HttpTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldPassForTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="HttpTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldPassForTestBuilder(HttpTestContext testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            this.testContext = testContext;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilder TheHttpContext(Action<HttpContext> assertions)
        {
            assertions(this.testContext.HttpContext);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilder TheHttpContext(Func<HttpContext, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.HttpContext, nameof(HttpContext));
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilder TheHttpRequest(Action<HttpRequest> assertions)
        {
            assertions(this.testContext.HttpRequest);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilder TheHttpRequest(Func<HttpRequest, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.HttpRequest, nameof(HttpRequest));
            return this;
        }
        
        /// <summary>
        /// Validates the object with the given predicate.
        /// </summary>
        /// <typeparam name="T">Type of the object to validate.</typeparam>
        /// <param name="predicate">Predicate to use for the object validation.</param>
        /// <param name="obj">Object to validate.</param>
        /// <param name="objectName">Optional name of the object to use in case of failed validation.</param>
        protected void ValidateFor<T>(Func<T, bool> predicate, T obj, string objectName = null)
        {
            if (!predicate(obj))
            {
                throw new InvalidAssertionException($"Expected the {objectName ?? obj.GetName()} to pass the given predicate but it failed.");
            }
        }
    }
}
