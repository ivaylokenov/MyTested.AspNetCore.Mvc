namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    using System.IO;
    using Base;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing file result.
    /// </summary>
    public interface IFileTestBuilder : IBaseTestBuilderWithActionResult<FileResult>
    {
        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether file result has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithFileDownloadName(string fileDownloadName);

        /// <summary>
        /// Tests whether file result has the same file stream as the provided one.
        /// </summary>
        /// <param name="stream">File stream.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithStream(Stream stream);

        /// <summary>
        /// Tests whether file result has the same file name as the provided one.
        /// </summary>
        /// <param name="fileName">File name as string.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithFileName(string fileName);

        /// <summary>
        /// Tests whether file result has the same file provider as the provided one.
        /// </summary>
        /// <param name="fileProvider">File provider of type IFileProvider.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider);

        /// <summary>
        /// Tests whether file result has the same file provider type as the provided one.
        /// </summary>
        /// <typeparam name="TFileProvider">File provider of type IFileProvider.</typeparam>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithFileProviderOfType<TFileProvider>()
            where TFileProvider : IFileProvider;

        /// <summary>
        /// Tests whether file result has the same file contents as the provided byte array.
        /// </summary>
        /// <param name="fileContents">File contents as byte array.</param>
        /// <returns>The same file test builder.</returns>
        IAndFileTestBuilder WithContents(byte[] fileContents);
    }
}
