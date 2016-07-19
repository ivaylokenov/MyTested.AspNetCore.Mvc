namespace MyTested.AspNetCore.Mvc.Builders.ExceptionErrors
{
    using System;
    using Base;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing expected exceptions.
    /// </summary>
    public class ExceptionTestBuilder : BaseTestBuilderWithInvokedAction, IAndExceptionTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ExceptionTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder OfType<TException>()
        {
            var expectedExceptionType = typeof(TException);
            var actualExceptionType = this.CaughtException.GetType();
            if (Reflection.AreDifferentTypes(expectedExceptionType, actualExceptionType))
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2}, but instead received {3}.",
                    this.ActionName,
                    this.Component.GetName(),
                    expectedExceptionType.ToFriendlyTypeName(),
                    this.CaughtException.GetName()));
            }

            return this;
        }

        /// <inheritdoc />
        public IExceptionMessageTestBuilder WithMessage()
        {
            return new ExceptionMessageTestBuilder(
                this.TestContext,
                this);
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder WithMessage(string message)
        {
            var actualExceptionMessage = this.CaughtException.Message;
            if (actualExceptionMessage != message)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected exception with message '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Component.GetName(),
                    message,
                    actualExceptionMessage));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder WithMessage(Action<string> assertions)
        {
            assertions(this.CaughtException.Message);
            return this;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder WithMessage(Func<string, bool> predicate)
        {
            var actualExceptionMessage = this.CaughtException.Message;
            if (!predicate(actualExceptionMessage))
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected exception message ('{2}') to pass the given predicate, but it failed.",
                    this.ActionName,
                    this.Component.GetName(),
                    actualExceptionMessage));
            }

            return this;
        }

        /// <inheritdoc />
        public IExceptionTestBuilder AndAlso() => this;
    }
}
