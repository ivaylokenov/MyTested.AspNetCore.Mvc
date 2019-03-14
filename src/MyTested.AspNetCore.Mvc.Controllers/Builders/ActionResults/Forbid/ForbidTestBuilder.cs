namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Forbid
{
    using Base;
    using Contracts.ActionResults.Forbid;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ForbidResult"/>.
    /// </summary>
    public class ForbidTestBuilder
        : BaseTestBuilderWithAuthenticationResult<ForbidResult, IAndForbidTestBuilder>, IAndForbidTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbidTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ForbidTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        protected override IAndForbidTestBuilder AuthenticationResultTestBuilder => this;

        /// <inheritdoc />
        public IForbidTestBuilder AndAlso() => this;

        protected override void ThrowNewAuthenticationResultAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new ForbidResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "forbid",
                propertyName,
                expectedValue,
                actualValue));
    }
}
