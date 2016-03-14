namespace MyTested.Mvc.Builders.ActionResults.Forbid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.ActionResults.Forbid;
    using Contracts.Authentication;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

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
        /// <param name="forbidResult">Result from the tested action.</param>
        public ForbidTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Tests whether forbid result contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same forbid test builder.</returns>
        public IAndForbidTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewForbidResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether forbid result has the provided enumerable of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as enumerable.</param>
        /// <returns>The same forbid test builder.</returns>
        public IAndForbidTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                   this.ActionResult,
                   authenticationSchemes,
                   this.ThrowNewForbidResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether forbid result has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same forbid test builder.</returns>
        public IAndForbidTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
            => this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());

        /// <summary>
        /// Tests whether forbid result has the provided authentication properties.
        /// </summary>
        /// <param name="properties">Expected authentication properties.</param>
        /// <returns>The same forbid test builder.</returns>
        public IAndForbidTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewForbidResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether forbid result has the provided authentication properties provided as builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Expected authentication properties.</param>
        /// <returns>The same forbid test builder.</returns>
        public IAndForbidTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                authenticationPropertiesBuilder,
                this.TestContext);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining forbid tests.
        /// </summary>
        /// <returns>The same forbid test builder.</returns>
        public IForbidTestBuilder AndAlso() => this;

        private void ThrowNewForbidResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new ForbidResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected forbid result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
