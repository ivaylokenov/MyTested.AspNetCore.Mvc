namespace MyTested.AspNetCore.Mvc.Builders.ExceptionErrors
{
    using System;
    using System.Linq;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="AggregateException"/>.
    /// </summary>
    public class AggregateExceptionTestBuilder : ExceptionTestBuilder, IAndAggregateExceptionTestBuilder
    {
        private readonly AggregateException aggregateException;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateExceptionTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AggregateExceptionTestBuilder(ControllerTestContext testContext)
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
                    "When calling {0} action in {1} expected AggregateException to contain {2}, but none was found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedInnerExceptionType.ToFriendlyTypeName()));
            }

            return this;
        }

        /// <inheritdoc />
        public new IAggregateExceptionTestBuilder AndAlso() => this;
    }
}
