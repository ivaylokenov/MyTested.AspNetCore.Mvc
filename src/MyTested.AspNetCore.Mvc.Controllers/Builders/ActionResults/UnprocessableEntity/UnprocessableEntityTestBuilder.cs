namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.UnprocessableEntity
{
    using System;
    using Base;
    using Contracts.ActionResults.UnprocessableEntity;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing unprocessable entity results.
    /// </summary>
    /// <typeparam name="TUnprocessableEntityResult">
    /// Type of unprocessable entity result - <see cref="UnprocessableEntityResult"/>
    /// or <see cref="UnprocessableEntityObjectResult"/>.
    /// </typeparam>
    public class UnprocessableEntityTestBuilder<TUnprocessableEntityResult>
        : BaseTestBuilderWithErrorResult<TUnprocessableEntityResult>,
        IAndUnprocessableEntityTestBuilder,
        IBaseTestBuilderWithErrorResultInternal<IAndUnprocessableEntityTestBuilder>
        where TUnprocessableEntityResult : ActionResult
    {
        private const string ActionResultName = "unprocessable entity";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityTestBuilder{TUnprocessableEntityResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public UnprocessableEntityTestBuilder(ControllerTestContext testContext)
            : base(testContext, ActionResultName)
        {
        }

        /// <summary>
        /// Gets the unprocessable entity result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndUnprocessableEntityTestBuilder"/> type.</value>
        public IAndUnprocessableEntityTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<UnprocessableEntityObjectResult> assertions)
        {
            this.GetObjectResult();
            return this.Passing<UnprocessableEntityObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<UnprocessableEntityObjectResult, bool> predicate)
        {
            this.GetObjectResult();
            return this.Passing<UnprocessableEntityObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IUnprocessableEntityTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="UnprocessableEntityResultAssertionException"/> for the
        /// provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new UnprocessableEntityResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                ActionResultName,
                propertyName,
                expectedValue,
                actualValue));
    }
}
