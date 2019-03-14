namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using System;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;
    using Uri;

    /// <summary>
    /// Used for testing <see cref="LocalRedirectResult"/>.
    /// </summary>
    public interface ILocalRedirectTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> is permanent.
        /// </summary>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder Permanent();

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has specific URL provided as string.
        /// </summary>
        /// <param name="localUrl">Expected URL as string.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrl(string localUrl);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> URL passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the URL.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrlPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> URL passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the URL.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrlPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has specific URL provided as <see cref="Uri"/>.
        /// </summary>
        /// <param name="localUrl">Expected URL as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Uri localUrl);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URL.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has the same <see cref="IUrlHelper"/> as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type <see cref="IUrlHelper"/>.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type <see cref="IUrlHelper"/>.</typeparam>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;
    }
}
