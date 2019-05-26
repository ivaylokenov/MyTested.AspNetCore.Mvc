namespace MyTested.AspNetCore.Mvc.Builders.ErrorMessages
{
    using Base;
    using Contracts.Base;
    using Contracts.ErrorMessages;
    using Exceptions;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing specific text error messages.
    /// </summary>
    public class ErrorMessageTestBuilder<TTestBuilder>
        : BaseTestBuilderWithComponent, IErrorMessageTestBuilder<TTestBuilder>
        where TTestBuilder : IBaseTestBuilderWithComponent
    {
        private readonly string actualMessage;
        private readonly TTestBuilder testBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessageTestBuilder{TTestBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="actualMessage">The actual text error message received from the error result.</param>
        /// <param name="testBuilder">Test builder to use as a return result.</param>
        public ErrorMessageTestBuilder(
            ComponentTestContext testContext,
            string actualMessage,
            TTestBuilder testBuilder)
            : base(testContext)
        {
            this.actualMessage = actualMessage;
            this.testBuilder = testBuilder;
        }

        /// <inheritdoc />
        public TTestBuilder ThatEquals(string errorMessage)
        {
            if (this.actualMessage != errorMessage)
            {
                this.ThrowErrorMessageAssertionException(
                    "{0} error message to be '{1}', but instead found '{2}'.",
                    errorMessage);
            }

            return this.testBuilder;
        }

        /// <inheritdoc />
        public TTestBuilder BeginningWith(string beginMessage)
        {
            if (!this.actualMessage.StartsWith(beginMessage))
            {
                this.ThrowErrorMessageAssertionException(
                    "{0} error message to begin with '{1}', but instead found '{2}'.",
                    beginMessage);
            }

            return this.testBuilder;
        }

        /// <inheritdoc />
        public TTestBuilder EndingWith(string endMessage)
        {
            if (!this.actualMessage.EndsWith(endMessage))
            {
                this.ThrowErrorMessageAssertionException(
                    "{0} error message to end with '{1}', but instead found '{2}'.",
                    endMessage);
            }

            return this.testBuilder;
        }

        /// <inheritdoc />
        public TTestBuilder Containing(string containsMessage)
        {
            if (!this.actualMessage.Contains(containsMessage))
            {
                this.ThrowErrorMessageAssertionException(
                    "{0} error message to contain '{1}', but instead found '{2}'.",
                    containsMessage);
            }

            return this.testBuilder;
        }

        private void ThrowErrorMessageAssertionException(string messageFormat, string operation)
            => throw new ErrorMessageAssertionException(string.Format(
                messageFormat,
                this.TestContext.ExceptionMessagePrefix,
                operation,
                this.actualMessage));
    }
}
