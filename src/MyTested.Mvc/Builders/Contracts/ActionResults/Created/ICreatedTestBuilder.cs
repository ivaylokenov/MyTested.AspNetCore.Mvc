namespace MyTested.Mvc.Builders.Contracts.ActionResults.Created
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Base;
    using Microsoft.AspNet.Mvc;
    using Uris;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    public interface ICreatedTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ObjectResult>
    {
        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(string location);

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(Uri location);

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether created at action result has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtAction(string actionName);
        
        /// <summary>
        /// Tests whether created at action result has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtController(string controllerName);

        /// <summary>
        /// Tests whether created at route result has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithRouteName(string routeName);
        
        /// <summary>
        /// Tests whether created result contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithRouteValue(string key);
        
        /// <summary>
        /// Tests whether created result contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithRouteValue(string key, object value);

        /// <summary>
        /// Tests whether created result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithRouteValues(object routeValues);

        /// <summary>
        /// Tests whether created result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithRouteValues(IDictionary<string, object> routeValues);
        
        /// <summary>
        /// Tests whether created result has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether created result has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type IUrlHelper.</typeparam>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller;

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller;
    }
}
