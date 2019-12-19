﻿namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Authentication
{
    using Builders.Base;
    using Contracts.ActionResults.Authentication;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="SignInResult"/>.
    /// </summary>
    public class SignInTestBuilder 
        : BaseTestBuilderWithActionResult<SignInResult>,
        IAndSignInTestBuilder,
        IBaseTestBuilderWithAuthenticationResultInternal<IAndSignInTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">
        /// <see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.
        /// </param>
        public SignInTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the sign in result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndSignInTestBuilder"/> type.</value>
        public IAndSignInTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public ISignInTestBuilder AndAlso() => this;

        /// <summary>
        /// Throws new <see cref="SignInResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            =>throw new SignInResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "sign in",
                propertyName,
                expectedValue,
                actualValue));
    }
}
