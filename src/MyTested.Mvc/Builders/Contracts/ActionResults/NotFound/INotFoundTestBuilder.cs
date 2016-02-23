namespace MyTested.Mvc.Builders.Contracts.ActionResults.NotFound
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    public interface INotFoundTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>Base test builder with action.</returns>
        IAndNotFoundTestBuilder WithNoResponseModel();

        /// <summary>
        /// Tests whether HTTP not found result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether HTTP not found has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether HTTP not found result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether HTTP not found result contains the content type provided as MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as enumerable of MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as MediaTypeHeaderValue parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether HTTP not found result contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of IOutputFormatter.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether HTTP not found result contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of IOutputFormatter.</typeparam>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether HTTP not found result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of IOutputFormatter.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether HTTP not found result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Output formatter parameters.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
