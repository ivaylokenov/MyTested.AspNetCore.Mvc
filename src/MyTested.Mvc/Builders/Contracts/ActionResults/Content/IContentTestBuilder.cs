namespace MyTested.Mvc.Builders.Contracts.ActionResults.Content
{
    using System.Net;
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
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        IAndContentTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether content result has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether content result has the same content type as the provided string.
        /// </summary>
        /// <param name="contenType">Content type as string.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        IAndContentTestBuilder WithContentType(string contenType);

        /// <summary>
        /// Tests whether content result has the same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contenType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        IAndContentTestBuilder WithContentType(MediaTypeHeaderValue contenType);
    }
}
