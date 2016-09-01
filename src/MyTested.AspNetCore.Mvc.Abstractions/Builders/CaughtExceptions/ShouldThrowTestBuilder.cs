namespace MyTested.AspNetCore.Mvc.Builders.CaughtExceptions
{
    using System;
    using Base;
    using Contracts.CaughtExceptions;
    using Exceptions;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing whether method invocation throws <see cref="System.Exception"/>.
    /// </summary>
    public class ShouldThrowTestBuilder : BaseTestBuilderWithComponent, IShouldThrowTestBuilder
    {
        private readonly IExceptionTestBuilder exceptionTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldThrowTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldThrowTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.exceptionTestBuilder = new ExceptionTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IExceptionTestBuilder Exception()
        {
            return this.exceptionTestBuilder;
        }

        /// <inheritdoc />
        public IAggregateExceptionTestBuilder AggregateException(int? withNumberOfInnerExceptions = null)
        {
            this.exceptionTestBuilder.OfType<AggregateException>();
            var aggregateException = this.TestContext.CaughtException as AggregateException;
            var innerExceptionsCount = aggregateException.InnerExceptions.Count;
            if (withNumberOfInnerExceptions.HasValue &&
                withNumberOfInnerExceptions != innerExceptionsCount)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "{0} {1} to contain {2} inner exceptions, but in fact contained {3}.",
                    this.TestContext.ExceptionMessagePrefix,
                    nameof(AggregateException),
                    withNumberOfInnerExceptions,
                    innerExceptionsCount));
            }

            return new AggregateExceptionTestBuilder(this.TestContext);
        }
    }
}
