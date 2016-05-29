namespace MyTested.Mvc.Builders.Contracts.ActionResults.File
{
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="PhysicalFileResult"/>.
    /// </summary>
    public interface IPhysicalFileTestBuilder : IBaseTestBuilderWithActionResult<PhysicalFileResult>
    {
        /// <summary>
        /// Tests whether <see cref="PhysicalFileResult"/> has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndPhysicalFileTestBuilder"/>.</returns>
        IAndPhysicalFileTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="PhysicalFileResult"/> has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndPhysicalFileTestBuilder"/>.</returns>
        IAndPhysicalFileTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="PhysicalFileResult"/> has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same <see cref="IAndPhysicalFileTestBuilder"/>.</returns>
        IAndPhysicalFileTestBuilder WithFileDownloadName(string fileDownloadName);

        /// <summary>
        /// Tests whether <see cref="PhysicalFileResult"/> has the same physical file path as the provided one.
        /// </summary>
        /// <param name="physicalPath">File physical path as string.</param>
        /// <returns>The same <see cref="IAndPhysicalFileTestBuilder"/>.</returns>
        IAndPhysicalFileTestBuilder WithPhysicalPath(string physicalPath);
    }
}
