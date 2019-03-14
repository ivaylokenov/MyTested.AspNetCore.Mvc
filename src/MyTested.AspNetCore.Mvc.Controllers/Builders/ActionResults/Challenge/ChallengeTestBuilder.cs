namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Challenge
{
    using Base;
    using Contracts.ActionResults.Challenge;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ChallengeResult"/>.
    /// </summary>
    public class ChallengeTestBuilder
        : BaseTestBuilderWithAuthenticationResult<ChallengeResult, IAndChallengeTestBuilder>, IAndChallengeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ChallengeTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        protected override IAndChallengeTestBuilder AuthenticationResultTestBuilder => this;

        /// <inheritdoc />
        public IChallengeTestBuilder AndAlso() => this;

        protected override void ThrowNewAuthenticationResultAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new ChallengeResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "challenge",
                propertyName,
                expectedValue,
                actualValue));
    }
}
