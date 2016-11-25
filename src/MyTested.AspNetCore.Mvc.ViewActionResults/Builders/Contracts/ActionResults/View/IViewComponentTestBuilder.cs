namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>.
    /// </summary>
    public interface IViewComponentTestBuilder : IBaseTestBuilderWithResponseModel
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> has the same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> has the same <see cref="IViewEngine"/> as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type <see cref="IViewEngine"/>.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        [Obsolete("The ViewEngine property is unused and will be removed in the next major version of ASP.NET Core MVC.")]
        IAndViewComponentTestBuilder WithViewEngine(IViewEngine viewEngine);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type <see cref="IViewEngine"/>.</typeparam>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        [Obsolete("The ViewEngine property is unused and will be removed in the next major version of ASP.NET Core MVC.")]
        IAndViewComponentTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine;

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with an argument with the same name as the provided one.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArgumentWithName(string name);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with an argument deeply equal to the provided one.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Expected argument value.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArgument(string name, object value);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with an argument equal to the provided one.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="argument">Argument object.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArgument<TArgument>(TArgument argument);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with an argument of the provided type.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>();

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with an argument of the provided type and the given name.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="name">Name of the argument.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>(string name);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with the provided arguments.
        /// </summary>
        /// <param name="arguments">Arguments object.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArguments(object arguments);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> will be invoked with the provided arguments.
        /// </summary>
        /// <param name="arguments">Argument objects as dictionary.</param>
        /// <returns>The same <see cref="IAndViewComponentTestBuilder"/>.</returns>
        IAndViewComponentTestBuilder ContainingArguments(IDictionary<string, object> arguments);
    }
}
