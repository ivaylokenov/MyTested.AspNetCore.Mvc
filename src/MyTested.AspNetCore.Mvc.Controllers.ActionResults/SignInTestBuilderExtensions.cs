namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Security.Claims;
    using Builders.ActionResults.Authentication;
    using Builders.Authentication;
    using Builders.Contracts.ActionResults.Authentication;
    using Builders.Contracts.Authentication;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Contains extension methods for <see cref="ISignInTestBuilder"/>.
    /// </summary>
    public static class SignInTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>
        /// has the same authentication scheme as the provided one.
        /// </summary>
        /// <param name="signInTestBuilder">
        /// Instance of <see cref="ISignInTestBuilder"/> type.
        /// </param>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same <see cref="IAndSignInTestBuilder"/>.</returns>
        public static IAndSignInTestBuilder WithAuthenticationScheme(
            this ISignInTestBuilder signInTestBuilder,
            string authenticationScheme)
        {
            var actualBuilder = (SignInTestBuilder)signInTestBuilder;

            var actualAuthenticationScheme = actualBuilder.ActionResult.AuthenticationScheme;

            if (actualAuthenticationScheme != authenticationScheme)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "authentication scheme",
                    $"to be {authenticationScheme.GetErrorMessageName()}",
                    $"instead received {actualAuthenticationScheme.GetErrorMessageName()}");
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>
        /// has the provided <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="signInTestBuilder">
        /// Instance of <see cref="ISignInTestBuilder"/> type.
        /// </param>
        /// <param name="principal">Expected <see cref="ClaimsPrincipal"/>.</param>
        /// <returns>The same <see cref="IAndSignInTestBuilder"/>.</returns>
        public static IAndSignInTestBuilder WithPrincipal(
            this ISignInTestBuilder signInTestBuilder,
            ClaimsPrincipal principal)
        {
            var actualBuilder = (SignInTestBuilder)signInTestBuilder;

            var actualPrincipal = actualBuilder.ActionResult.Principal;

            if (Reflection.AreNotDeeplyEqual(principal, actualPrincipal))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "principal",
                    "to be the same as the provided one",
                    "instead received different result");
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/> has
        /// the given <see cref="ClaimsPrincipal"/> provided as builder.
        /// </summary>
        /// <param name="signInTestBuilder">
        /// Instance of <see cref="ISignInTestBuilder"/> type.
        /// </param>
        /// <param name="principalBuilder">Expected <see cref="ClaimsPrincipal"/> builder.</param>
        /// <returns>The same <see cref="IAndSignInTestBuilder"/>.</returns>
        public static IAndSignInTestBuilder WithPrincipal(
            this ISignInTestBuilder signInTestBuilder,
            Action<IClaimsPrincipalBuilder> principalBuilder)
        {
            var actualBuilder = (SignInTestBuilder)signInTestBuilder;
            
            var newClaimsPrincipalBuilder = new ClaimsPrincipalBuilder();
            principalBuilder(newClaimsPrincipalBuilder);

            var expectedPrincipal = newClaimsPrincipalBuilder.GetClaimsPrincipal();
            
            return actualBuilder.WithPrincipal(expectedPrincipal);
        }
    }
}
