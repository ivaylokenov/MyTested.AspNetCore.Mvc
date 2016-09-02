namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.BadRequest
{
    using Base;
    using Contracts.ActionResults.BadRequest;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing specific bad request text error messages.
    /// </summary>
    public class BadRequestErrorMessageTestBuilder
        : BaseTestBuilderWithComponent, IBadRequestErrorMessageTestBuilder
    {
        private readonly string actualMessage;
        private readonly IAndBadRequestTestBuilder badRequestTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestErrorMessageTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="actualMessage">Actual text error message received from bad request result.</param>
        /// <param name="badRequestTestBuilder">Bad request test builder.</param>
        public BadRequestErrorMessageTestBuilder(
            ComponentTestContext testContext,
            string actualMessage,
            IAndBadRequestTestBuilder badRequestTestBuilder)
            : base(testContext)
        {
            this.actualMessage = actualMessage;
            this.badRequestTestBuilder = badRequestTestBuilder;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder ThatEquals(string errorMessage)
        {
            if (this.actualMessage != errorMessage)
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to be '{2}', but instead found '{3}'.",
                    errorMessage);
            }

            return this.badRequestTestBuilder;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder BeginningWith(string beginMessage)
        {
            if (!this.actualMessage.StartsWith(beginMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to begin with '{2}', but instead found '{3}'.",
                    beginMessage);
            }

            return this.badRequestTestBuilder;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder EndingWith(string endMessage)
        {
            if (!this.actualMessage.EndsWith(endMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to end with '{2}', but instead found '{3}'.",
                    endMessage);
            }

            return this.badRequestTestBuilder;
        }

        /// <inheritdoc />
        public IAndBadRequestTestBuilder Containing(string containsMessage)
        {
            if (!this.actualMessage.Contains(containsMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }

            return this.badRequestTestBuilder;
        }

        private void ThrowNewBadRequestResultAssertionException(string messageFormat, string operation)
        {
            throw new BadRequestResultAssertionException(string.Format(
                messageFormat,
                this.TestContext.MethodName,
                this.TestContext.Component.GetName(),
                operation,
                this.actualMessage));
        }
    }
}
