namespace MyTested.AspNetCore.Mvc.Builders.CaughtExceptions
{
    using System;
    using Base;
    using Contracts.CaughtExceptions;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;

    /// <summary>
    /// Used for testing expected exceptions.
    /// </summary>
    public class ExceptionTestBuilder : BaseTestBuilderWithComponent, IAndExceptionTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ExceptionTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder OfType(Type exceptionType)
        {
            var actualExceptionType = this.TestContext.CaughtException.GetType();
            if (Reflection.AreDifferentTypes(exceptionType, actualExceptionType))
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "{0} {1}, but instead received {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    exceptionType.ToFriendlyTypeName(),
                    actualExceptionType.ToFriendlyTypeName()));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder OfType<TException>()
            => OfType(typeof(TException));

        public IExceptionMessageTestBuilder WithMessage()
        {
            return new ExceptionMessageTestBuilder(
                this.TestContext,
                this);
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder WithMessage(string message)
        {
            var actualExceptionMessage = this.TestContext.CaughtException.Message;
            if (actualExceptionMessage != message)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "{0} exception with message '{1}', but instead received '{2}'.",
                    this.TestContext.ExceptionMessagePrefix,
                    message,
                    actualExceptionMessage));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder WithMessage(Action<string> assertions)
        {
            assertions(this.TestContext.CaughtException.Message);
            return this;
        }

        /// <inheritdoc />
        public IAndExceptionTestBuilder WithMessage(Func<string, bool> predicate)
        {
            var actualExceptionMessage = this.TestContext.CaughtException.Message;
            if (!predicate(actualExceptionMessage))
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "{0} exception message ('{1}') to pass the given predicate, but it failed.",
                    this.TestContext.ExceptionMessagePrefix,
                    actualExceptionMessage));
            }

            return this;
        }

        /// <inheritdoc />
        public IExceptionTestBuilder AndAlso() => this;
    }
}
