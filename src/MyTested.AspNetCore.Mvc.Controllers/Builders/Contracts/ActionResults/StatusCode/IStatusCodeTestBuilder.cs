namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.StatusCode
{
    using System.Collections.Generic;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="StatusCodeResult"/> or <see cref="ObjectResult"/>.
    /// </summary>
    public interface IStatusCodeTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether the action result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether the action result contains the content type provided as <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether the action result contains the same content types provided as collection of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of strings.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether the action result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether the action result contains the same content types provided as collection of <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether the action result contains the same content types provided as <see cref="MediaTypeHeaderValue"/> parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as <see cref="MediaTypeHeaderValue"/> parameters.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether the action result contains the provided <see cref="IOutputFormatter"/>.
        /// </summary>
        /// <param name="outputFormatter">Instance of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether the action result contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of <see cref="IOutputFormatter"/>.</typeparam>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether the action result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Collection of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether the action result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters"><see cref="IOutputFormatter"/> parameters.</param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        IAndStatusCodeTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
