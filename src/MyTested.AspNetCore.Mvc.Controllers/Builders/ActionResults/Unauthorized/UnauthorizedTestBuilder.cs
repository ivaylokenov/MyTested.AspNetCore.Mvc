namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Unauthorized
{
    using System;
    using Builders.Base;
    using Contracts.ActionResults.Unauthorized;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing unauthorized result.
    /// </summary>
    /// <typeparam name="TUnauthorizedResult">
    /// Type of unauthorized result - <see cref="UnauthorizedResult"/> or <see cref="UnauthorizedObjectResult"/>.
    /// </typeparam>
    public class UnauthorizedTestBuilder<TUnauthorizedResult>
        : BaseTestBuilderWithResponseModel<TUnauthorizedResult>,
        IAndUnauthorizedTestBuilder,
        IBaseTestBuilderWithOutputResultInternal<IAndUnauthorizedTestBuilder>
        where TUnauthorizedResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedTestBuilder{TUnauthorizedResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public UnauthorizedTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the unauthorized result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndUnauthorizedTestBuilder"/>.</value>
        public IAndUnauthorizedTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<UnauthorizedObjectResult> assertions)
        {
            this.ValidateUnauthorizedObjectResult();
            return this.Passing<UnauthorizedObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<UnauthorizedObjectResult, bool> predicate)
        {
            this.ValidateUnauthorizedObjectResult();
            return this.Passing<UnauthorizedObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IUnauthorizedTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<UnauthorizedResult>();

        /// <summary>
        /// Throws new <see cref="UnauthorizedResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new UnauthorizedResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "unauthorized",
                propertyName,
                expectedValue,
                actualValue));
        
        private void ValidateUnauthorizedObjectResult()
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(UnauthorizedObjectResult);

            if (actualResultType != expectedResultType)
            {
                throw new UnauthorizedResultAssertionException(string.Format(
                    "{0} unauthorized result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
