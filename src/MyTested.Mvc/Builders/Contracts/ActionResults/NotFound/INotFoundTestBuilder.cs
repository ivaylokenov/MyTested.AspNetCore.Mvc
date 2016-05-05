namespace MyTested.Mvc.Builders.Contracts.ActionResults.NotFound
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.
    /// </summary>
    public interface INotFoundTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder WithNoResponseModel();

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the content type provided as <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the same content types provided as collection of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of strings.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the same content types provided as collection of <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of MediaTypeHeaderValue.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the same content types provided as <see cref="MediaTypeHeaderValue"/> parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the provided <see cref="IOutputFormatter"/>.
        /// </summary>
        /// <param name="outputFormatter">Instance of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of <see cref="IOutputFormatter"/>.</typeparam>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Collection of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether <see cref="NotFoundObjectResult"/> contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters"><see cref="IOutputFormatter"/> parameters.</param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
