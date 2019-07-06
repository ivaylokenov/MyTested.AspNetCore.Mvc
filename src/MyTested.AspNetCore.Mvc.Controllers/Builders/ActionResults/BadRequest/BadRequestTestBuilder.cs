namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.BadRequest
{
    using System;
    using Base;
    using Contracts.ActionResults.BadRequest;
    using Contracts.And;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing bad request results.
    /// </summary>
    /// <typeparam name="TBadRequestResult">
    /// Type of bad request result - <see cref="BadRequestResult"/>
    /// or <see cref="BadRequestObjectResult"/>.
    /// </typeparam>
    public class BadRequestTestBuilder<TBadRequestResult> 
        : BaseTestBuilderWithErrorResult<TBadRequestResult>,
        IAndBadRequestTestBuilder,
        IBaseTestBuilderWithErrorResultInternal<IAndBadRequestTestBuilder>
        where TBadRequestResult : ActionResult
    {
        private const string ActionResultName = "bad request";

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestTestBuilder{TBadRequestResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public BadRequestTestBuilder(ControllerTestContext testContext)
            : base(testContext, ActionResultName)
        {
        }

        /// <summary>
        /// Gets the bad request result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndBadRequestTestBuilder"/> type.</value>
        public IAndBadRequestTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<BadRequestObjectResult> assertions)
        {
            this.GetObjectResult();
            return this.Passing<BadRequestObjectResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<BadRequestObjectResult, bool> predicate)
        {
            this.GetObjectResult();
            return this.Passing<BadRequestObjectResult>(predicate);
        }

        /// <inheritdoc />
        public IBadRequestTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="BadRequestResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new BadRequestResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                ActionResultName,
                propertyName,
                expectedValue,
                actualValue));
    }
}
