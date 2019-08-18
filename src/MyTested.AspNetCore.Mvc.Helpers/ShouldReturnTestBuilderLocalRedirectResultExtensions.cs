namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderLocalRedirectResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>
        /// with the same local redirect URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="localUrl">Expected local redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder LocalRedirect<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string localUrl)
            => shouldReturnTestBuilder
                .LocalRedirect(result => result
                    .ToUrl(localUrl)
                    .Permanent(false)
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>
        /// with a permanent redirect and the same local URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="localUrl">Expected local redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder LocalRedirectPermanent<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string localUrl)
            => shouldReturnTestBuilder
                .LocalRedirect(result => result
                    .ToUrl(localUrl)
                    .Permanent()
                    .PreservingMethod(false));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>
        /// with a preserved method and the same local URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="localUrl">Expected local redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder LocalRedirectPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string localUrl)
            => shouldReturnTestBuilder
                .LocalRedirect(result => result
                    .ToUrl(localUrl)
                    .Permanent(false)
                    .PreservingMethod());

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>
        /// with a preserved method, permanent redirect, and the same local URL as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="localUrl">Expected local redirect URL.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder LocalRedirectPermanentPreserveMethod<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string localUrl)
            => shouldReturnTestBuilder
                .LocalRedirect(result => result
                    .ToUrl(localUrl)
                    .Permanent()
                    .PreservingMethod());
    }
}
