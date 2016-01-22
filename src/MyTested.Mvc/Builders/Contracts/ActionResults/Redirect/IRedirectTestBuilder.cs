namespace MyTested.Mvc.Builders.Contracts.ActionResults.Redirect
{
    using System;
    using System.Collections.Generic;
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
        /// Tests whether redirect result is permanent.
        /// </summary>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder Permanent();

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
        /// Tests whether redirect at action result has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder ToAction(string actionName);
        
        /// <summary>
        /// Tests whether redirect at action result has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder ToController(string controllerName);
        
        /// <summary>
        /// Tests whether redirect at route result has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithRouteName(string routeName);
        
        /// <summary>
        /// Tests whether redirect result contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithRouteValue(string key);
        
        /// <summary>
        /// Tests whether redirect result contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithRouteValue(string key, object value);
        
        /// <summary>
        /// Tests whether redirect result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithRouteValues(object routeValues);

        /// <summary>
        /// Tests whether redirect result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithRouteValues(IDictionary<string, object> routeValues);
        
        /// <summary>
        /// Tests whether redirect result has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether redirect result has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type IUrlHelper.</typeparam>
        /// <returns>The same redirect test builder.</returns>
        IAndRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;

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
