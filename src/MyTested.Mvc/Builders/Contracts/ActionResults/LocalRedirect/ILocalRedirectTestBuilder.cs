namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using Uris;
    using System;

    /// <summary>
    /// Used for testing local redirect result.
    /// </summary>
    public interface ILocalRedirectTestBuilder
    {
        /// <summary>
        /// Tests whether local redirect result is permanent.
        /// </summary>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder Permanent();

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as string.
        /// </summary>
        /// <param name="localUrl">Expected URL as string.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder To(string localUrl);

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as URI.
        /// </summary>
        /// <param name="localUrl">Expected URL as URI.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder To(Uri localUrl);

        /// <summary>
        /// Tests whether local redirect result has specific URL provided by builder.
        /// </summary>
        /// <param name="localUrlTestBuilder">Builder for expected URl.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder To(Action<IUriTestBuilder> localUrlTestBuilder);
    }
}
