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
    /// Used for testing <see cref="ForbidResult"/>.
    /// </summary>
    public class ForbidTestBuilder
        : BaseTestBuilderWithActionResult<ForbidResult>, IAndForbidTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbidTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        public ForbidTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndForbidTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewForbidResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndForbidTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                   this.ActionResult,
                   authenticationSchemes,
                   this.ThrowNewForbidResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndForbidTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
            => this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());

        /// <inheritdoc />
        public IAndForbidTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewForbidResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndForbidTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                authenticationPropertiesBuilder,
                this.TestContext);

            return this;
        }

        /// <inheritdoc />
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
