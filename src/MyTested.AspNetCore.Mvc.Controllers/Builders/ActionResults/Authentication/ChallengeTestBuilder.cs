namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Authentication
{
    using Builders.Base;
    using Contracts.ActionResults.Authentication;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ChallengeResult"/>.
    /// </summary>
    public class ChallengeTestBuilder
        : BaseTestBuilderWithActionResult<ChallengeResult>,
        IAndChallengeTestBuilder,
        IBaseTestBuilderWithAuthenticationResultInternal<IAndChallengeTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public ChallengeTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the challenge result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndChallengeTestBuilder"/> type.</value>
        public IAndChallengeTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IChallengeTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="ChallengeResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue) 
            => throw new ChallengeResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "challenge",
                propertyName,
                expectedValue,
                actualValue));
    }
}
