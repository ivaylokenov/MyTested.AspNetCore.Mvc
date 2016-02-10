namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using System;
    using Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing local redirect result.
    /// </summary>
    public interface ILocalRedirectTestBuilder : IBaseTestBuilderWithActionResult<LocalRedirectResult>
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
        IAndLocalRedirectTestBuilder ToUrl(string localUrl);

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as URI.
        /// </summary>
        /// <param name="localUrl">Expected URL as URI.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Uri localUrl);
    }
}
