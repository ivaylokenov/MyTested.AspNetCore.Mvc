namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Conflict
{
    using Base;
    using Contracts.ActionResults.Conflict;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Used for testing conflict result.
    /// </summary>
    /// <typeparam name="TConflictResult">
    /// Type of conflict result - <see cref="ConflictResult"/> or <see cref="ConflictObjectResult"/>.
    /// </typeparam>
    public class ConflictTestBuilder<TConflictResult>
        : BaseTestBuilderWithResponseModel<TConflictResult>,
        IAndConflictTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndConflictTestBuilder>
        where TConflictResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictTestBuilder{TConflictResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public ConflictTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the conflict result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndConflictTestBuilder"/>.</value>
        public IAndConflictTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<ConflictObjectResult> assertions)
        {
            this.ValidateConflictObjectResult();
            return this.Passing<ConflictObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<ConflictObjectResult, bool> predicate)
        {
            this.ValidateConflictObjectResult();
            return this.Passing<ConflictObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IConflictTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<ConflictResult>();

        /// <summary>
        /// Throws new <see cref="ConflictResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new ConflictResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "conflict",
                propertyName,
                expectedValue,
                actualValue));

        private void ValidateConflictObjectResult()
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(ConflictObjectResult);

            if (actualResultType != expectedResultType)
            {
                throw new ConflictResultAssertionException(string.Format(
                    "{0} conflict result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
