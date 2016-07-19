namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Challenge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.ActionResults.Challenge;
    using Contracts.Authentication;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="ChallengeResult"/>.
    /// </summary>
    public class ChallengeTestBuilder
        : BaseTestBuilderWithActionResult<ChallengeResult>, IAndChallengeTestBuilder
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
        public IAndChallengeTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndChallengeTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                   this.ActionResult,
                   authenticationSchemes,
                   this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndChallengeTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
            => this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());

        /// <inheritdoc />
        public IAndChallengeTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndChallengeTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                authenticationPropertiesBuilder,
                this.TestContext);

            return this;
        }

        /// <inheritdoc />
        public IChallengeTestBuilder AndAlso() => this;

        private void ThrowNewChallengeResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new ChallengeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected challenge result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Component.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
