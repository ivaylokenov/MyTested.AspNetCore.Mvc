namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.LocalRedirect;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="ILocalRedirectTestBuilder"/>.
    /// </summary>
    public static class LocalRedirectTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="LocalRedirectResult"/>
        /// has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <param name="localRedirectTestBuilder">
        /// Instance of <see cref="ILocalRedirectTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        public static IAndLocalRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>(
            this ILocalRedirectTestBuilder localRedirectTestBuilder)
            where TUrlHelper : IUrlHelper
            => localRedirectTestBuilder
                .WithUrlHelperOfType<IAndLocalRedirectTestBuilder, TUrlHelper>();
    }
}
