namespace MyTested.Mvc.Builders.ActionResults.Challenge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.ActionResults.Challenge;
    using Contracts.Authentication;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Http.Authentication;
    using Microsoft.AspNet.Mvc;
    using Utilities.Validators;

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
        /// <param name="actionResult">Result from the tested action.</param>
        public ChallengeTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            ChallengeResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
        
        public IAndChallengeTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewChallengeResultAssertionException);

            return this;
        }
        
        public IAndChallengeTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                   this.ActionResult,
                   authenticationSchemes,
                   this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        public IAndChallengeTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
        {
            return this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());
        }

        public IAndChallengeTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewChallengeResultAssertionException);

            return this;
        }

        public IAndChallengeTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                authenticationPropertiesBuilder,
                this.Controller,
                this.ActionName);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining challenge tests.
        /// </summary>
        /// <returns>The same challenge test builder.</returns>
        public IChallengeTestBuilder AndAlso()
        {
            return this;
        }

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
