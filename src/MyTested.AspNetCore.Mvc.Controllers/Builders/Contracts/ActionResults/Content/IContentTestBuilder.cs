namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Content
{
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="ContentResult"/>.
    /// </summary>
    public interface IContentTestBuilder : IBaseTestBuilderWithStatusCodeResult<IAndContentTestBuilder>
    {
        /// <summary>
        /// Tests whether <see cref="ContentResult"/> has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        IAndContentTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="ContentResult"/> has the same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        IAndContentTestBuilder WithContentType(MediaTypeHeaderValue contentType);
    }
}
