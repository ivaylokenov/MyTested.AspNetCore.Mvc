namespace MyTested.Mvc.Builders.ActionResults.Challenge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.ActionResults.Challenge;
    using Contracts.Authentication;
    using Exceptions;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing challenge result.
    /// </summary>
    public class ChallengeTestBuilder
        : BaseTestBuilderWithActionResult<ChallengeResult>, IAndChallengeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="challengeResult">Result from the tested action.</param>
        public ChallengeTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Tests whether challenge result contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same challenge test builder.</returns>
        public IAndChallengeTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether challenge result has the provided enumerable of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as enumerable.</param>
        /// <returns>The same challenge test builder.</returns>
        public IAndChallengeTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                   this.ActionResult,
                   authenticationSchemes,
                   this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether challenge result has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same challenge test builder.</returns>
        public IAndChallengeTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
            => this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());

        /// <summary>
        /// Tests whether challenge result has the provided authentication properties.
        /// </summary>
        /// <param name="properties">Expected authentication properties.</param>
        /// <returns>The same challenge test builder.</returns>
        public IAndChallengeTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether challenge result has the provided authentication properties provided as builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Expected authentication properties.</param>
        /// <returns>The same challenge test builder.</returns>
        public IAndChallengeTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                authenticationPropertiesBuilder,
                this.TestContext);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining challenge tests.
        /// </summary>
        /// <returns>The same challenge test builder.</returns>
        public IChallengeTestBuilder AndAlso() => this;

        private void ThrowNewChallengeResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new ChallengeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected challenge result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
