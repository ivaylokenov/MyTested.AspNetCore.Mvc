namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.AspNetCore.Authentication;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderForbidResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>
        /// with the same authentication schemes as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Forbid<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            params string[] authenticationSchemes)
            => shouldReturnTestBuilder
                .Forbid(result => result
                    .ContainingAuthenticationSchemes(authenticationSchemes));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>
        /// with the same authentication properties as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="properties">Expected authentication properties.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Forbid<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            AuthenticationProperties properties)
            => shouldReturnTestBuilder
                .Forbid(result => result
                    .WithAuthenticationProperties(properties));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>
        /// with the same authentication schemes and properties as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="properties">Expected authentication properties.</param>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Forbid<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            AuthenticationProperties properties,
            params string[] authenticationSchemes)
            => shouldReturnTestBuilder
                .Forbid(result => result
                    .WithAuthenticationProperties(properties)
                    .ContainingAuthenticationSchemes(authenticationSchemes));
    }
}
