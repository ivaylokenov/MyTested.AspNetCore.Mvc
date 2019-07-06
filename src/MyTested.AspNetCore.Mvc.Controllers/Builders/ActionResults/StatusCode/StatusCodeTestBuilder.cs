namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.StatusCode
{
    using System;
    using Builders.Base;
    using Contracts.ActionResults.StatusCode;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing status code result.
    /// </summary>
    /// <typeparam name="TStatusCodeResult">
    /// Type of status code result - <see cref="StatusCodeResult"/> or <see cref="ObjectResult"/>.
    /// </typeparam>
    public class StatusCodeTestBuilder<TStatusCodeResult>
        : BaseTestBuilderWithResponseModel<TStatusCodeResult>,
        IAndStatusCodeTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndStatusCodeTestBuilder>
        where TStatusCodeResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCodeTestBuilder{TStatusCodeResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public StatusCodeTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the status code result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndStatusCodeTestBuilder"/>.</value>
        public IAndStatusCodeTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<StatusCodeResult> assertions)
        {
            this.ValidateActionResult<StatusCodeResult>();
            return this.Passing<StatusCodeResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<StatusCodeResult, bool> predicate)
        {
            this.ValidateActionResult<StatusCodeResult>();
            return this.Passing<StatusCodeResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<ObjectResult> assertions)
        {
            this.ValidateActionResult<ObjectResult>();
            return this.Passing<ObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<ObjectResult, bool> predicate)
        {
            this.ValidateActionResult<ObjectResult>();
            return this.Passing<ObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IStatusCodeTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<StatusCodeResult>();

        /// <summary>
        /// Throws new <see cref="StatusCodeResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new StatusCodeResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "status code",
                propertyName,
                expectedValue,
                actualValue));

        private void ValidateActionResult<TResult>()
            where TResult : ActionResult
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(TResult);

            if (actualResultType != expectedResultType)
            {
                throw new RedirectResultAssertionException(string.Format(
                    "{0} action result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
