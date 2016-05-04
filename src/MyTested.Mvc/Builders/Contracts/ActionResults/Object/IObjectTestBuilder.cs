namespace MyTested.Mvc.Builders.Contracts.ActionResults.Object
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="ObjectResult"/>.
    /// </summary>
    public interface IObjectTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ObjectResult>
    {
        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether object has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the content type provided as <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the same content types provided as enumerable of <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the same content types provided as <see cref="MediaTypeHeaderValue"/> parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as <see cref="MediaTypeHeaderValue"/> parameters.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the provided <see cref="IOutputFormatter"/>.
        /// </summary>
        /// <param name="outputFormatter">Instance of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of <see cref="IOutputFormatter"/>.</typeparam>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether <see cref="ObjectResult"/> contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters"><see cref="IOutputFormatter"/> parameters.</param>
        /// <returns>The same <see cref="IAndObjectTestBuilder"/>.</returns>
        IAndObjectTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
