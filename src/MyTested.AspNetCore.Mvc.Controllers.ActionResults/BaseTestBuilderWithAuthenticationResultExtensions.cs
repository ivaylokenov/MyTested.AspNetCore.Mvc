namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Authentication;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Authentication;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithAuthenticationResult{TAuthenticationResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithAuthenticationResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="baseTestBuilderWithAuthenticationResult">
        /// Instance of <see cref="IBaseTestBuilderWithAuthenticationResult{TAuthenticationResultTestBuilder}"/> type.
        /// </param>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TAuthenticationResultTestBuilder ContainingAuthenticationScheme<TAuthenticationResultTestBuilder>(
            this IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> baseTestBuilderWithAuthenticationResult,
            string authenticationScheme)
            where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder =
                (IBaseTestBuilderWithAuthenticationResultInternal<TAuthenticationResultTestBuilder>)baseTestBuilderWithAuthenticationResult;

            AuthenticationValidator.ValidateAuthenticationScheme(
                actualBuilder.TestContext.MethodResult,
                authenticationScheme,
                actualBuilder.ThrowNewAuthenticationResultAssertionException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the provided collection of authentication schemes.
        /// </summary>
        /// <param name="baseTestBuilderWithAuthenticationResult">
        /// Instance of <see cref="IBaseTestBuilderWithAuthenticationResult{TAuthenticationResultTestBuilder}"/> type.
        /// </param>
        /// <param name="authenticationSchemes">Expected authentication schemes as collection.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TAuthenticationResultTestBuilder ContainingAuthenticationSchemes<TAuthenticationResultTestBuilder>(
            this IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> baseTestBuilderWithAuthenticationResult,
            IEnumerable<string> authenticationSchemes)
            where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder =
                (IBaseTestBuilderWithAuthenticationResultInternal<TAuthenticationResultTestBuilder>)baseTestBuilderWithAuthenticationResult;

            AuthenticationValidator.ValidateAuthenticationSchemes(
                actualBuilder.TestContext.MethodResult,
                authenticationSchemes,
                actualBuilder.ThrowNewAuthenticationResultAssertionException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="baseTestBuilderWithAuthenticationResult">
        /// Instance of <see cref="IBaseTestBuilderWithAuthenticationResult{TAuthenticationResultTestBuilder}"/> type.
        /// </param>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TAuthenticationResultTestBuilder ContainingAuthenticationSchemes<TAuthenticationResultTestBuilder>(
            this IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> baseTestBuilderWithAuthenticationResult,
            params string[] authenticationSchemes)
            where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithAuthenticationResult
                .ContainingAuthenticationSchemes(authenticationSchemes.AsEnumerable());

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the provided <see cref="AuthenticationProperties"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithAuthenticationResult">
        /// Instance of <see cref="IBaseTestBuilderWithAuthenticationResult{TAuthenticationResultTestBuilder}"/> type.
        /// </param>
        /// <param name="properties">Expected <see cref="AuthenticationProperties"/>.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TAuthenticationResultTestBuilder WithAuthenticationProperties<TAuthenticationResultTestBuilder>(
            this IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> baseTestBuilderWithAuthenticationResult,
            AuthenticationProperties properties)
            where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder =
                (IBaseTestBuilderWithAuthenticationResultInternal<TAuthenticationResultTestBuilder>)baseTestBuilderWithAuthenticationResult;

            AuthenticationValidator.ValidateAuthenticationProperties(
                actualBuilder.TestContext.MethodResult,
                properties,
                actualBuilder.ThrowNewAuthenticationResultAssertionException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> has
        /// the given <see cref="AuthenticationProperties"/> provided as builder.
        /// </summary>
        /// <param name="baseTestBuilderWithAuthenticationResult">
        /// Instance of <see cref="IBaseTestBuilderWithAuthenticationResult{TAuthenticationResultTestBuilder}"/> type.
        /// </param>
        /// <param name="authenticationPropertiesBuilder">Expected <see cref="AuthenticationProperties"/> builder.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TAuthenticationResultTestBuilder WithAuthenticationProperties<TAuthenticationResultTestBuilder>(
            this IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> baseTestBuilderWithAuthenticationResult,
            Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder)
            where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder =
                (IBaseTestBuilderWithAuthenticationResultInternal<TAuthenticationResultTestBuilder>)baseTestBuilderWithAuthenticationResult;

            AuthenticationValidator.ValidateAuthenticationProperties(
                authenticationPropertiesBuilder,
                actualBuilder.TestContext);

            return actualBuilder.ResultTestBuilder;
        }
    }
}
