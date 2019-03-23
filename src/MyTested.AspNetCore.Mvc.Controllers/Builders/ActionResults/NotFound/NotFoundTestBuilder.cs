namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.NotFound
{
    using Base;
    using Contracts.ActionResults.NotFound;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    /// <typeparam name="THttpNotFoundResult">Type of not found result - <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.</typeparam>
    public class NotFoundTestBuilder<THttpNotFoundResult>
        : BaseTestBuilderWithOutputResult<THttpNotFoundResult, IAndNotFoundTestBuilder>, IAndNotFoundTestBuilder
        where THttpNotFoundResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public NotFoundTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the HTTP not found result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndNotFoundTestBuilder"/>.</value>
        protected override IAndNotFoundTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndNotFoundTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<NotFoundResult>();

        /// <summary>
        /// Throws new HTTP not found result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed..</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewHttpNotFoundResultAssertionException(propertyName, expectedValue, actualValue);

        private void ThrowNewHttpNotFoundResultAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new NotFoundResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "HTTP not found",
                propertyName,
                expectedValue,
                actualValue));
    }
}
