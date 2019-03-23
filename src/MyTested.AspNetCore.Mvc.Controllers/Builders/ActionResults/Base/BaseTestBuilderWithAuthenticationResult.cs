namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Base;
    using Contracts.ActionResults.Base;
    using Contracts.Authentication;
    using Contracts.Base;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with authentication action result.
    /// </summary>
    /// <typeparam name="TAuthenticationResult">Authentication action result from invoked action in ASP.NET Core MVC controller.</typeparam>
    /// <typeparam name="TAuthenticationResultTestBuilder">Type of authentication result test builder to use as a return type for common methods.</typeparam>
    public abstract class BaseTestBuilderWithAuthenticationResult<TAuthenticationResult, TAuthenticationResultTestBuilder>
        : BaseTestBuilderWithActionResult<TAuthenticationResult>, 
        IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder>
        where TAuthenticationResult : ActionResult
        where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BaseTestBuilderWithAuthenticationResult{TAuthenticationResult, TAuthenticationResultTestBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithAuthenticationResult(ControllerTestContext testContext) 
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the authentication action result test builder.
        /// </summary>
        /// <value>Test builder for the authentication action result.</value>
        protected abstract TAuthenticationResultTestBuilder AuthenticationResultTestBuilder { get; }

        /// <inheritdoc />
        public TAuthenticationResultTestBuilder ContainingAuthenticationScheme(string authenticationScheme)
        {
            AuthenticationValidator.ValidateAuthenticationScheme(
                this.ActionResult,
                authenticationScheme,
                this.ThrowNewAuthenticationResultAssertionException);

            return this.AuthenticationResultTestBuilder;
        }

        /// <inheritdoc />
        public TAuthenticationResultTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            AuthenticationValidator.ValidateAuthenticationSchemes(
                this.ActionResult,
                authenticationSchemes,
                this.ThrowNewAuthenticationResultAssertionException);

            return this.AuthenticationResultTestBuilder;
        }

        /// <inheritdoc />
        public TAuthenticationResultTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes)
            => this.ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());

        /// <inheritdoc />
        public TAuthenticationResultTestBuilder WithAuthenticationProperties(AuthenticationProperties properties)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                this.ActionResult,
                properties,
                this.ThrowNewAuthenticationResultAssertionException);

            return this.AuthenticationResultTestBuilder;
        }

        /// <inheritdoc />
        public TAuthenticationResultTestBuilder WithAuthenticationProperties(
            Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
        {
            AuthenticationValidator.ValidateAuthenticationProperties(
                authenticationPropertiesBuilder,
                this.TestContext);

            return this.AuthenticationResultTestBuilder;
        }

        protected abstract void ThrowNewAuthenticationResultAssertionException(
            string propertyName, 
            string expectedValue,
            string actualValue);
    }
}
