namespace MyTested.Mvc.Builders.ExceptionErrors
{
    using Base;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="System.Exception"/> messages.
    /// </summary>
    public class ExceptionMessageTestBuilder
        : BaseTestBuilderWithInvokedAction, IExceptionMessageTestBuilder
    {
        private readonly IAndExceptionTestBuilder exceptionTestBuilder;
        private readonly string actualMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        /// <param name="exceptionTestBuilder">Test builder of <see cref="IAndExceptionTestBuilder"/> type.</param>
        public ExceptionMessageTestBuilder(
            ControllerTestContext testContext,
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
                    "When calling {0} action in {1} expected exception message to be '{2}', but instead found '{3}'.",
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
                    "When calling {0} action in {1} expected exception message to begin with '{2}', but instead found '{3}'.",
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
                    "When calling {0} action in {1} expected exception message to end with '{2}', but instead found '{3}'.",
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
                    "When calling {0} action in {1} expected exception message to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }

            return this.exceptionTestBuilder;
        }

        private void ThrowNewInvalidExceptionAssertionException(string messageFormat, string operation)
        {
            throw new InvalidExceptionAssertionException(string.Format(
                messageFormat,
                this.ActionName,
                this.Controller.GetName(),
                operation,
                this.actualMessage));
        }
    }
}
