namespace MyTested.Mvc.Builders.Contracts.ActionResults.Ok
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing ok result.
    /// </summary>
    public interface IOkTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder WithNoResponseModel();

        /// <summary>
        /// Tests whether ОК result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether ОК has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether ОК result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether ОК result contains the content type provided as MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether ОК result contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether ОК result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether ОК result contains the same content types provided as enumerable of MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether ОК result contains the same content types provided as MediaTypeHeaderValue parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether ОК result contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of IOutputFormatter.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether ОК result contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of IOutputFormatter.</typeparam>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether ОК result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of IOutputFormatter.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether ОК result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Output formatter parameters.</param>
        /// <returns>The same ОК test builder.</returns>
        IAndOkTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
