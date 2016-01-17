namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNet.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;

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

        IAndViewComponentTestBuilder WithArgument<TArgument>(TArgument argument);

        IAndViewComponentTestBuilder WithArgumentOfType<TArgument>();

        IAndViewComponentTestBuilder WithArguments(IEnumerable<object> arguments);

        IAndViewComponentTestBuilder WithArguments(params object[] arguments);
    }
}
