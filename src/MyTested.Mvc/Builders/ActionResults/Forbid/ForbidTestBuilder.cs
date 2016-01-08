namespace MyTested.Mvc.Builders.ActionResults.Forbid
{
    using Microsoft.AspNet.Mvc;
    using Base;
    using Contracts.ActionResults.Forbid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Http.Authentication;
    using Contracts.Authentication;
    using Utilities.Validators;
    using Exceptions;
    using Internal.Extensions;

    /// <summary>
    /// Used for testing forbid result.
    /// </summary>
    public class ForbidTestBuilder
        : BaseTestBuilderWithActionResult<ForbidResult>, IAndForbidTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbidTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ForbidTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            ForbidResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IAndForbidTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewForbidResultAssertionException);

            return this;
        }

        // TODO: add to interface, add documentation, unit tests
        public IAndForbidTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                   this.ActionResult,
                   authenticationSchemes,
                   this.ThrowNewForbidResultAssertionException);

            return this;
        }

        public IAndForbidTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
        {
            return this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());
        }

        public IAndForbidTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewForbidResultAssertionException);

            return this;
        }

        public IAndForbidTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                authenticationPropertiesBuilder,
                this.Controller,
                this.ActionName);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining forbid tests.
        /// </summary>
        /// <returns>The same forbid test builder.</returns>
        public IForbidTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewForbidResultAssertionException(string propertyName, string expectedValue, string actualValue)
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
