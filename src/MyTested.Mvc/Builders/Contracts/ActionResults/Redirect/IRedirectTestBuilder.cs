namespace MyTested.Mvc.Builders.Contracts.ActionResults.Redirect
{
    using System;
    using System.Linq.Expressions;
    using Base;
    using Microsoft.AspNet.Mvc;
    using Uris;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    public interface IRedirectTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether redirect result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder ToUrl(string location);

        /// <summary>
        /// Tests whether redirect result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder ToUrl(Uri location);

        /// <summary>
        /// Tests whether redirect result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller;

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller;
    }
}
