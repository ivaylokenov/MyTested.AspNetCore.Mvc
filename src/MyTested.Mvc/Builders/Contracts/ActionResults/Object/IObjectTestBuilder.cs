namespace MyTested.Mvc.Builders.Contracts.ActionResults.Object
{
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Collections.Generic;
    using System.Net;

    public interface IObjectTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ObjectResult>
    {
        /// <summary>
        /// Tests whether object result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether object has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether object result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether object result contains the content type provided as MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether object result contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether object result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether object result contains the same content types provided as enumerable of MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether object result contains the same content types provided as MediaTypeHeaderValue parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether object result contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of IOutputFormatter.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether object result contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of IOutputFormatter.</typeparam>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether object result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of IOutputFormatter.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether object result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Output formatter parameters.</param>
        /// <returns>The same object test builder.</returns>
        IAndObjectTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
