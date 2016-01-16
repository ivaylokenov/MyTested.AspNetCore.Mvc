namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing physical file result.
    /// </summary>
    public interface IPhysicalFileTestBuilder
    {
        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same file test builder.</returns>
        IAndPhysicalFileTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same file test builder.</returns>
        IAndPhysicalFileTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether file result has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same file test builder.</returns>
        IAndPhysicalFileTestBuilder WithFileDownloadName(string fileDownloadName);

        /// <summary>
        /// Tests whether file result has the same physical file path as the provided one.
        /// </summary>
        /// <param name="physicalPath">File physical path as string.</param>
        /// <returns>The same file test builder.</returns>
        IAndPhysicalFileTestBuilder WithPhysicalPath(string physicalPath);
    }
}
