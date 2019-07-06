namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.AspNetCore.Authentication;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderSignOutResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/>
        /// with the same authentication schemes as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder SignOut<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            params string[] authenticationSchemes)
            => shouldReturnTestBuilder
                .SignOut(result => result
                    .ContainingAuthenticationSchemes(authenticationSchemes));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/>
        /// with the same authentication properties and schemes as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="properties">Expected authentication properties.</param>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder SignOut<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            AuthenticationProperties properties,
            params string[] authenticationSchemes)
            => shouldReturnTestBuilder
                .SignOut(result => result
                    .WithAuthenticationProperties(properties)
                    .ContainingAuthenticationSchemes(authenticationSchemes));
    }
}
