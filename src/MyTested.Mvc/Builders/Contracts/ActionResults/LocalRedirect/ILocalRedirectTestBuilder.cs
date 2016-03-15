namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Uris;

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
        /// Tests whether local redirect result URL passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the URL.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder ToUrlPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether local redirect result URL passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the URL.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder ToUrlPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether local redirect result has specific URL provided as URI.
        /// </summary>
        /// <param name="localUrl">Expected URL as URI.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Uri localUrl);

        /// <summary>
        /// Tests whether local redirect result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether local redirect result has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether local redirect result has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type IUrlHelper.</typeparam>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;

        /// <summary>
        /// Tests whether local redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same local redirect test builder.</returns>
        IAndLocalRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : class;

        IAndLocalRedirectTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class;
    }
}
