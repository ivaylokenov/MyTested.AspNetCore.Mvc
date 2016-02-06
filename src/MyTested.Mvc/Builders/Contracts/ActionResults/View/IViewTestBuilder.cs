namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing view results.
    /// </summary>
    public interface IViewTestBuilder : IBaseTestBuilderWithViewFeature
    {
        /// <summary>
        /// Tests whether view result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same view test builder.</returns>
        IAndViewTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether view result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same view test builder.</returns>
        IAndViewTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether view result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same view test builder.</returns>
        IAndViewTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether view result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same view test builder.</returns>
        IAndViewTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether view result has the same view engine as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type IViewEngine.</param>
        /// <returns>The same view test builder.</returns>
        IAndViewTestBuilder WithViewEngine(IViewEngine viewEngine);

        /// <summary>
        /// Tests whether view result has the same view engine type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type IViewEngine.</typeparam>
        /// <returns>The same view test builder.</returns>
        IAndViewTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine;
    }
}
