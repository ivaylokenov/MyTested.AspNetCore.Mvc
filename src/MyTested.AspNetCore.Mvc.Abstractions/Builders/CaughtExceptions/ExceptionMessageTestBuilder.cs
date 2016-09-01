namespace MyTested.AspNetCore.Mvc.Builders.CaughtExceptions
{
    using Base;
    using Contracts.CaughtExceptions;
    using Exceptions;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing <see cref="System.Exception"/> messages.
    /// </summary>
    public class ExceptionMessageTestBuilder
        : BaseTestBuilderWithComponent, IExceptionMessageTestBuilder
    {
        private readonly IAndExceptionTestBuilder exceptionTestBuilder;
        private readonly string actualMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="exceptionTestBuilder">Test builder of <see cref="IAndExceptionTestBuilder"/> type.</param>
        public ExceptionMessageTestBuilder(
            ComponentTestContext testContext,
            IAndExceptionTestBuilder exceptionTestBuilder)
            : base(testContext)
        {
            this.exceptionTestBuilder = exceptionTestBuilder;
            this.actualMessage = testContext.CaughtException.Message;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder ThatEquals(string errorMessage)
        {
            if (this.actualMessage != errorMessage)
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "{0} exception message to be '{1}', but instead found '{2}'.",
                    errorMessage);
            }

            return this.exceptionTestBuilder;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder BeginningWith(string beginMessage)
        {
            if (!this.actualMessage.StartsWith(beginMessage))
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "{0} exception message to begin with '{1}', but instead found '{2}'.",
                    beginMessage);
            }

            return this.exceptionTestBuilder;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder EndingWith(string endMessage)
        {
            if (!this.actualMessage.EndsWith(endMessage))
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "{0} exception message to end with '{1}', but instead found '{2}'.",
                    endMessage);
            }

            return this.exceptionTestBuilder;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder Containing(string containsMessage)
        {
            if (!this.actualMessage.Contains(containsMessage))
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "{0} exception message to contain '{1}', but instead found '{2}'.",
                    containsMessage);
            }

            return this.exceptionTestBuilder;
        }

        private void ThrowNewInvalidExceptionAssertionException(string messageFormat, string operation)
        {
            throw new InvalidExceptionAssertionException(string.Format(
                messageFormat,
                this.TestContext.ExceptionMessagePrefix,
                operation,
                this.actualMessage));
        }
    }
}
