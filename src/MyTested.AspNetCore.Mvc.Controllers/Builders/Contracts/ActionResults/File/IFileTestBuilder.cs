namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    using System.IO;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="FileResult"/>, <see cref="FileContentResult"/>, <see cref="FileStreamResult"/> or <see cref="VirtualFileResult"/>.
    /// </summary>
    public interface IFileTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether <see cref="FileResult"/>, <see cref="FileContentResult"/> or <see cref="FileStreamResult"/> has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="FileResult"/>, <see cref="FileContentResult"/> or <see cref="FileStreamResult"/> has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="FileResult"/>, <see cref="FileContentResult"/> or <see cref="FileStreamResult"/> has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithDownloadName(string fileDownloadName);

        /// <summary>
        /// Tests whether <see cref="FileStreamResult"/> has the same file stream as the provided one.
        /// </summary>
        /// <param name="stream">File stream.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithStream(Stream stream);

        /// <summary>
        /// Tests whether <see cref="VirtualFileResult"/> has the same file name as the provided one.
        /// </summary>
        /// <param name="fileName">File name as string.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithName(string fileName);

        /// <summary>
        /// Tests whether <see cref="VirtualFileResult"/> has the same file provider as the provided one.
        /// </summary>
        /// <param name="fileProvider">File provider of type <see cref="IFileProvider"/>.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider);

        /// <summary>
        /// Tests whether <see cref="VirtualFileResult"/> has the same file provider type as the provided one.
        /// </summary>
        /// <typeparam name="TFileProvider">File provider of type <see cref="IFileProvider"/>.</typeparam>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithFileProviderOfType<TFileProvider>()
            where TFileProvider : IFileProvider;

        /// <summary>
        /// Tests whether <see cref="FileContentResult"/> has the same file contents as the provided byte array.
        /// </summary>
        /// <param name="fileContents">File contents as byte array.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithContents(byte[] fileContents);
    }
}
