namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Accepted
{
    using System;
    using Base;
    using Contracts.ActionResults.Accepted;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing accepted results.
    /// </summary>
    /// <typeparam name="TAcceptedResult">
    /// Type of created result - <see cref="AcceptedResult"/>,
    /// <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>.
    /// </typeparam>
    public class AcceptedTestBuilder<TAcceptedResult>
        : BaseTestBuilderWithResponseModel<TAcceptedResult>,
        IAndAcceptedTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndAcceptedTestBuilder>,
        IBaseTestBuilderWithRouteValuesResultInternal<IAndAcceptedTestBuilder>
        where TAcceptedResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptedTestBuilder{TAcceptedResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public AcceptedTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public bool IncludeCountCheck { get; set; } = true;

        /// <summary>
        /// Gets the accepted result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndAcceptedTestBuilder"/> type.</value>
        public IAndAcceptedTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<AcceptedResult> assertions)
        {
            this.ValidateAcceptedResult<AcceptedResult>();
            return this.Passing<AcceptedResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<AcceptedResult, bool> predicate)
        {
            this.ValidateAcceptedResult<AcceptedResult>();
            return this.Passing<AcceptedResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<AcceptedAtRouteResult> assertions)
        {
            this.ValidateAcceptedResult<AcceptedAtRouteResult>();
            return this.Passing<AcceptedAtRouteResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<AcceptedAtRouteResult, bool> predicate)
        {
            this.ValidateAcceptedResult<AcceptedAtRouteResult>();
            return this.Passing<AcceptedAtRouteResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<AcceptedAtActionResult> assertions)
        {
            this.ValidateAcceptedResult<AcceptedAtActionResult>();
            return this.Passing<AcceptedAtActionResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<AcceptedAtActionResult, bool> predicate)
        {
            this.ValidateAcceptedResult<AcceptedAtActionResult>();
            return this.Passing<AcceptedAtActionResult>(predicate);
        }

        /// <inheritdoc />
        public IAcceptedTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="AcceptedResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new AcceptedResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "accepted",
                propertyName,
                expectedValue,
                actualValue));

        private void ValidateAcceptedResult<TResult>()
            where TResult : ObjectResult
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(TResult);

            if (actualResultType != expectedResultType)
            {
                throw new AcceptedResultAssertionException(string.Format(
                    "{0} accepted result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
