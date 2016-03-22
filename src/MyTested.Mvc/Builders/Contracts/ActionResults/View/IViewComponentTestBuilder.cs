namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing view component results.
    /// </summary>
    public interface IViewComponentTestBuilder : IBaseTestBuilderWithViewFeature
    {
        /// <summary>
        /// Tests whether view component result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether view component result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether view component result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether view component result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether view component result has the same view engine as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type IViewEngine.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder WithViewEngine(IViewEngine viewEngine);

        /// <summary>
        /// Tests whether view component result has the same view engine type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type IViewEngine.</typeparam>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine;

        /// <summary>
        /// Tests whether view component result will be invoked with an argument with the same name as the provided one.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Expected argument value.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder ContainingArgumentWithName(string name);

        /// <summary>
        /// Tests whether view component result will be invoked with an argument deeply equal to the provided one.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Expected argument value.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder ContainingArgument(string name, object value);

        /// <summary>
        /// Tests whether view component result will be invoked with an argument equal to the provided one.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="argument">Argument object.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder ContainingArgument<TArgument>(TArgument argument);

        /// <summary>
        /// Tests whether view component result will be invoked with an argument of the provided type.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>();

        IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>(string name);

        /// <summary>
        /// Tests whether view component result will be invoked with the provided arguments.
        /// </summary>
        /// <param name="arguments">Arguments object.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder ContainingArguments(object arguments);

        /// <summary>
        /// Tests whether view component result will be invoked with the provided arguments.
        /// </summary>
        /// <param name="arguments">Argument objects as dictionary.</param>
        /// <returns>The same view component test builder.</returns>
        IAndViewComponentTestBuilder ContainingArguments(IDictionary<string, object> arguments);
    }
}
