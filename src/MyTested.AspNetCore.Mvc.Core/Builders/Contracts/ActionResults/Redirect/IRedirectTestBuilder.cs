namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Redirect
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Uris;

    /// <summary>
    /// Used for testing <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>.
    /// </summary>
    public interface IRedirectTestBuilder : IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> is permanent.
        /// </summary>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder Permanent();

        /// <summary>
        /// Tests whether <see cref="RedirectResult"/> has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToUrl(string location);

        /// <summary>
        /// Tests whether <see cref="RedirectResult"/> location passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the location.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToUrlPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="RedirectResult"/> location passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToUrlPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether <see cref="RedirectResult"/> has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToUrl(Uri location);

        /// <summary>
        /// Tests whether <see cref="RedirectResult"/> has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToAction(string actionName);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> result has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToController(string controllerName);

        /// <summary>
        /// Tests whether <see cref="RedirectToRouteResult"/> result has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder WithRouteName(string routeName);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteKey(string key);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains specific route value.
        /// </summary>
        /// <typeparam name="TRouteValue">Type of the route value.</typeparam>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteValue<TRouteValue>(TRouteValue value);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains specific route value of the given type.
        /// </summary>
        /// <typeparam name="TRouteValue">Expected type of the route value.</typeparam>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>();

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains specific route value of the given type with the provided key.
        /// </summary>
        /// <typeparam name="TRouteValue">Expected type of the route value.</typeparam>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>(string key);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteValue(string key, object value);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteValues(object routeValues);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ContainingRouteValues(IDictionary<string, object> routeValues);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> has the same <see cref="IUrlHelper"/> as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type <see cref="IUrlHelper"/>.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type <see cref="IUrlHelper"/>.</typeparam>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : class;

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/> redirects to specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous redirect action.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class;
    }
}
