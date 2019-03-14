namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.BadRequest
{
    using Builders.Base;
    using Contracts.ActionResults.BadRequest;
    using Exceptions;
    using Internal.TestContexts;

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
                    "{0} bad request error message to be '{1}', but instead found '{2}'.",
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
                    "{0} bad request error message to begin with '{1}', but instead found '{2}'.",
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
                    "{0} bad request error message to end with '{1}', but instead found '{2}'.",
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
                    "{0} bad request error message to contain '{1}', but instead found '{2}'.",
                    containsMessage);
            }

            return this.badRequestTestBuilder;
        }

        private void ThrowNewBadRequestResultAssertionException(string messageFormat, string operation)
        {
            throw new BadRequestResultAssertionException(string.Format(
                messageFormat,
                this.TestContext.ExceptionMessagePrefix,
                operation,
                this.actualMessage));
        }
    }
}
