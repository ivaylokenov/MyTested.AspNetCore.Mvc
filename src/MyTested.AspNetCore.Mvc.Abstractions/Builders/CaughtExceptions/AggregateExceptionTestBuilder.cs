namespace MyTested.AspNetCore.Mvc.Builders.CaughtExceptions
{
    using System;
    using System.Linq;
    using Contracts.CaughtExceptions;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;

    /// <summary>
    /// Used for testing <see cref="AggregateException"/>.
    /// </summary>
    public class AggregateExceptionTestBuilder : ExceptionTestBuilder, IAndAggregateExceptionTestBuilder
    {
        private readonly AggregateException aggregateException;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateExceptionTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public AggregateExceptionTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.aggregateException = testContext.CaughtExceptionAs<AggregateException>();
        }

        /// <inheritdoc />
        public IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
            where TInnerException : Exception
        {
            var expectedInnerExceptionType = typeof(TInnerException);
            var innerExceptionFound = this.aggregateException.InnerExceptions.Any(e => e.GetType() == expectedInnerExceptionType);
            if (!innerExceptionFound)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "{0} {1} to contain {2}, but none was found.",
                    this.TestContext.ExceptionMessagePrefix,
                    nameof(AggregateException),
                    expectedInnerExceptionType.ToFriendlyTypeName()));
            }

            return this;
        }

        /// <inheritdoc />
        public new IAggregateExceptionTestBuilder AndAlso() => this;
    }
}
