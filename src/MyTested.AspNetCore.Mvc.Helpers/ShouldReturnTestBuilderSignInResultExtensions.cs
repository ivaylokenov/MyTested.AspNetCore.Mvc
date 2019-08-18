namespace MyTested.AspNetCore.Mvc
{
    using System.Security.Claims;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.AspNetCore.Authentication;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderSignInResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>
        /// with the same claims principal and authentication scheme as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="principal">Expected claims principal.</param>
        /// <param name="authenticationScheme">Expected authentication scheme.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder SignIn<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            ClaimsPrincipal principal,
            string authenticationScheme)
            => shouldReturnTestBuilder
                .SignIn(result => result
                    .WithPrincipal(principal)
                    .WithAuthenticationScheme(authenticationScheme));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>
        /// with the same claims principal, authentication properties, and authentication scheme as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="principal">Expected claims principal.</param>
        /// <param name="properties">Expected authentication properties.</param>
        /// <param name="authenticationScheme">Expected authentication scheme.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder SignIn<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            ClaimsPrincipal principal,
            AuthenticationProperties properties,
            string authenticationScheme)
            => shouldReturnTestBuilder
                .SignIn(result => result
                    .WithPrincipal(principal)
                    .WithAuthenticationProperties(properties)
                    .WithAuthenticationScheme(authenticationScheme));
    }
}
