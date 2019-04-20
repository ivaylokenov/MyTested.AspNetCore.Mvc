namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.StatusCode
{
    using Base;
    using Contracts.ActionResults.StatusCode;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing status code result.
    /// </summary>
    /// <typeparam name="TStatusCodeResult">Type of status code result - <see cref="StatusCodeResult"/> or <see cref="ObjectResult"/>.</typeparam>
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
    }
}
