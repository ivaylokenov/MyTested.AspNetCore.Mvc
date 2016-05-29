namespace MyTested.Mvc.Builders.Actions
{
    using System;
    using Base;
    using Contracts.Actions;
    using Contracts.ExceptionErrors;
    using ExceptionErrors;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing whether action throws <see cref="System.Exception"/>.
    /// </summary>
    public class ShouldThrowTestBuilder : BaseTestBuilderWithInvokedAction, IShouldThrowTestBuilder
    {
        private readonly IExceptionTestBuilder exceptionTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldThrowTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldThrowTestBuilder(ControllerTestContext testContext)
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
            var aggregateException = this.CaughtException as AggregateException;
            var innerExceptionsCount = aggregateException.InnerExceptions.Count;
            if (withNumberOfInnerExceptions.HasValue &&
                withNumberOfInnerExceptions != innerExceptionsCount)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected AggregateException to contain {2} inner exceptions, but in fact contained {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    withNumberOfInnerExceptions,
                    innerExceptionsCount));
            }

            return new AggregateExceptionTestBuilder(this.TestContext);
        }
    }
}
