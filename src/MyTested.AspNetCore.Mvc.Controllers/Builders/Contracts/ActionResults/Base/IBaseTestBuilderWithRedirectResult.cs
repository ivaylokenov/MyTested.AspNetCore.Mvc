namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using System;
    using Contracts.Base;
    using Uri;

    /// <summary>
    /// Base interface for all test builders with redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TRedirectResultTestBuilder">Type of redirect result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> : 
        IBaseTestBuilderWithUrlHelperResult<TRedirectResultTestBuilder>
        where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> is permanent.
        /// </summary>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRedirectResultTestBuilder Permanent();

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRedirectResultTestBuilder ToUrl(string location);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// location passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the location.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRedirectResultTestBuilder ToUrlPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// location passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRedirectResultTestBuilder ToUrlPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRedirectResultTestBuilder ToUrl(Uri location);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TRedirectResultTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder);
    }
}
