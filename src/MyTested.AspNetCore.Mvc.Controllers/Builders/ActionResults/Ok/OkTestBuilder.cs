namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Ok
{
    using Base;
    using Contracts.ActionResults.Ok;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing OK result.
    /// </summary>
    /// <typeparam name="TOkResult">Type of OK result - <see cref="OkResult"/> or <see cref="OkObjectResult"/>.</typeparam>
    public class OkTestBuilder<TOkResult>
        : BaseTestBuilderWithOutputResult<TOkResult, IAndOkTestBuilder>, IAndOkTestBuilder
        where TOkResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkTestBuilder{TOkResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public OkTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the OK result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndOkTestBuilder"/>.</value>
        protected override IAndOkTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IOkTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<OkResult>();

        /// <summary>
        /// Throws new OK result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewOkResultAssertionException(propertyName, expectedValue, actualValue);

        private void ThrowNewOkResultAssertionException(string propertyName, string expectedValue, string actualValue)
            => throw new OkResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "OK",
                propertyName,
                expectedValue,
                actualValue));
    }
}
