namespace MyTested.Mvc.Builders.Contracts.ActionResults.Content
{
    using System.Net;
    using System.Text;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing content result.
    /// </summary>
    public interface IContentTestBuilder : IBaseTestBuilderWithActionResult<ContentResult>
    {
        /// <summary>
        /// Tests whether content result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether content result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether content result has the same content type as the provided string.
        /// </summary>
        /// <param name="contenType">Content type as string.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithContentType(string contenType);

        /// <summary>
        /// Tests whether content result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contenType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithContentType(MediaTypeHeaderValue contenType);
    }
}
