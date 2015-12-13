namespace MyTested.Mvc.Builders.Contracts.ActionResults.Content
{
    using System.Net;
    using Models;
    using System.Text;
    using Microsoft.Net.Http.Headers;
    using Base;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing content result.
    /// </summary>
    public interface IContentTestBuilder : IBaseTestBuilderWithActionResult<ContentResult>
    {
        /// <summary>
        /// Tests whether content result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode); // TODO: status code is int?

        /// <summary>
        /// Tests whether content result has the same content type as the provided string.
        /// </summary>
        /// <param name="mediaType">Media type as string.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithMediaType(string mediaType);

        /// <summary>
        /// Tests whether content result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="mediaType">Media type as MediaTypeHeaderValue.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithMediaType(MediaTypeHeaderValue mediaType);

        /// <summary>
        /// Tests whether content result has the default UTF8 encoding.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithDefaultEncoding();

        /// <summary>
        /// Tests whether content result has the same encoging as the provided Encoding.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithEncoding(Encoding encoding);
    }
}
