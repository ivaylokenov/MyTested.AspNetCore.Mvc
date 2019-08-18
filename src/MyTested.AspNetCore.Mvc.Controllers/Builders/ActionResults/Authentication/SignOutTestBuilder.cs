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
    /// Used for testing <see cref="SignOutResult"/>.
    /// </summary>
    public class SignOutTestBuilder 
        : BaseTestBuilderWithActionResult<SignOutResult>,
        IAndSignOutTestBuilder,
        IBaseTestBuilderWithAuthenticationResultInternal<IAndSignOutTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignOutTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public SignOutTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the sign out result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndSignInTestBuilder"/> type.</value>
        public IAndSignOutTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public ISignOutTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="SignOutResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new SignOutResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "sign out",
                propertyName,
                expectedValue,
                actualValue));
    }
}
