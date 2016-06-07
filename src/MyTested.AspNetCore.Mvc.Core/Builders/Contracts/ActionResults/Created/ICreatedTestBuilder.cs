namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Created
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Uris;

    /// <summary>
    /// Used for testing <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>.
    /// </summary>
    public interface ICreatedTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ObjectResult>
    {
        /// <summary>
        /// Tests whether <see cref="CreatedResult"/> has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocation(string location);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/> location passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the location.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocationPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/> location passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocationPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/> has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocation(Uri location);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/> has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtAction(string actionName);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder AtController(string controllerName);

        /// <summary>
        /// Tests whether <see cref="CreatedAtRouteResult"/> has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder WithRouteName(string routeName);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteKey(string key);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains specific route value.
        /// </summary>
        /// <typeparam name="TRouteValue">Type of the route value.</typeparam>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteValue<TRouteValue>(TRouteValue value);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains specific route value of the given type.
        /// </summary>
        /// <typeparam name="TRouteValue">Expected type of the route value.</typeparam>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteValueOfType<TRouteValue>();

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains specific route value of the given type with the provided key.
        /// </summary>
        /// <typeparam name="TRouteValue">Expected type of the route value.</typeparam>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteValueOfType<TRouteValue>(string key);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteValue(string key, object value);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteValues(object routeValues);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingRouteValues(IDictionary<string, object> routeValues);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type <see cref="IUrlHelper"/>.</typeparam>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : class;

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> returns created at specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous created action.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class;

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the content type provided as <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the same content types provided as collection of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of strings.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the same content types provided as collection of <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the same content types provided as <see cref="MediaTypeHeaderValue"/> parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as <see cref="MediaTypeHeaderValue"/> parameters.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of <see cref="IOutputFormatter"/>.</typeparam>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Collection of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/> contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters"><see cref="IOutputFormatter"/> parameters.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        IAndCreatedTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
