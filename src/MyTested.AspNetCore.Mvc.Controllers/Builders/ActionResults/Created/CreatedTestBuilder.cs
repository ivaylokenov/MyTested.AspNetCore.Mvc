namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Created
{
    using Builders.Base;
    using Contracts.ActionResults.Created;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    /// <typeparam name="TCreatedResult">
    /// Type of created result - <see cref="CreatedResult"/>,
    /// <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>.
    /// </typeparam>
    public class CreatedTestBuilder<TCreatedResult>
        : BaseTestBuilderWithResponseModel<TCreatedResult>,
        IAndCreatedTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndCreatedTestBuilder>,
        IBaseTestBuilderWithRouteValuesResultInternal<IAndCreatedTestBuilder>
        where TCreatedResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedTestBuilder{TCreatedResult}"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public CreatedTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public bool IncludeCountCheck { get; set; } = true;

        /// <summary>
        /// Gets the created result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndCreatedTestBuilder"/> type.</value>
        public IAndCreatedTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<CreatedResult> assertions)
        {
            this.ValidateCreatedResult<CreatedResult>();
            return this.Passing<CreatedResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<CreatedResult, bool> predicate)
        {
            this.ValidateCreatedResult<CreatedResult>();
            return this.Passing<CreatedResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<CreatedAtRouteResult> assertions)
        {
            this.ValidateCreatedResult<CreatedAtRouteResult>();
            return this.Passing<CreatedAtRouteResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<CreatedAtRouteResult, bool> predicate)
        {
            this.ValidateCreatedResult<CreatedAtRouteResult>();
            return this.Passing<CreatedAtRouteResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<CreatedAtActionResult> assertions)
        {
            this.ValidateCreatedResult<CreatedAtActionResult>();
            return this.Passing<CreatedAtActionResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<CreatedAtActionResult, bool> predicate)
        {
            this.ValidateCreatedResult<CreatedAtActionResult>();
            return this.Passing<CreatedAtActionResult>(predicate);
        }

        /// <inheritdoc />
        public ICreatedTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="CreatedResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new CreatedResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "created",
                propertyName,
                expectedValue,
                actualValue));

        private void ValidateCreatedResult<TResult>()
            where TResult : ObjectResult
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(TResult);

            if (actualResultType != expectedResultType)
            {
                throw new CreatedResultAssertionException(string.Format(
                    "{0} created result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
