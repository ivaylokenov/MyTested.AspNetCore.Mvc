namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Utilities.Extensions;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    public class ShouldPassForTestBuilderWithInvokedAction : ShouldPassForTestBuilderWithAction,
        IShouldPassForTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldPassForTestBuilderWithInvokedAction"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldPassForTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Action<Exception> assertions)
        {
            assertions(this.TestContext.CaughtException);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithInvokedAction TheCaughtException(Func<Exception, bool> predicate)
        {
            var exception = this.TestContext.CaughtException;
            this.ValidateFor(predicate, exception, exception != null ? exception.GetName() : "caught exception");
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Action<HttpResponse> assertions)
        {
            assertions(this.TestContext.HttpResponse);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithInvokedAction TheHttpResponse(Func<HttpResponse, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.HttpResponse, nameof(HttpResponse));
            return this;
        }
    }
}
