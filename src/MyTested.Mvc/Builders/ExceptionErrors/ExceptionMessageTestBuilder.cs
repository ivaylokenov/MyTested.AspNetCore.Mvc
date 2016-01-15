namespace MyTested.Mvc.Builders.ExceptionErrors
{
    using System;
    using Base;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing specific exception messages.
    /// </summary>
    public class ExceptionMessageTestBuilder
        : BaseTestBuilderWithCaughtException, IExceptionMessageTestBuilder
    {
        private readonly IAndExceptionTestBuilder exceptionTestBuilder;
        private readonly string actualMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="exceptionTestBuilder">Original exception test builder.</param>
        public ExceptionMessageTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            IAndExceptionTestBuilder exceptionTestBuilder)
            : base(controller, actionName, caughtException)
        {
            this.exceptionTestBuilder = exceptionTestBuilder;
            this.actualMessage = caughtException.Message;
        }

        /// <summary>
        /// Tests whether particular exception message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular exception.</param>
        /// <returns>Exception test builder.</returns>
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

        /// <summary>
        /// Tests whether particular exception message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
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

        /// <summary>
        /// Tests whether particular exception message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
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

        /// <summary>
        /// Tests whether particular exception message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
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
