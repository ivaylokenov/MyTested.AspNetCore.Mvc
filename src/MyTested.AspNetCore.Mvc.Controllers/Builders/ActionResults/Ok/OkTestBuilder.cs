namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Ok
{
    using System;
    using Base;
    using Contracts.ActionResults.Ok;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing OK result.
    /// </summary>
    /// <typeparam name="TOkResult">
    /// Type of OK result - <see cref="OkResult"/> or <see cref="OkObjectResult"/>.
    /// </typeparam>
    public class OkTestBuilder<TOkResult>
        : BaseTestBuilderWithResponseModel<TOkResult>,
        IAndOkTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndOkTestBuilder>
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
        public IAndOkTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<OkObjectResult> assertions)
        {
            this.ValidateOkObjectResult();
            return this.Passing<OkObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<OkObjectResult, bool> predicate)
        {
            this.ValidateOkObjectResult();
            return this.Passing<OkObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IOkTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<OkResult>();

        /// <summary>
        /// Throws new <see cref="OkResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new OkResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "OK",
                propertyName,
                expectedValue,
                actualValue));
        
        private void ValidateOkObjectResult()
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(OkObjectResult);

            if (actualResultType != expectedResultType)
            {
                throw new NotFoundResultAssertionException(string.Format(
                    "{0} OK result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
