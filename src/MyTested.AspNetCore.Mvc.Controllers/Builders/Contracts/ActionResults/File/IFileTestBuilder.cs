namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    using System.IO;
    using Microsoft.Extensions.FileProviders;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>, <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>.
    /// </summary>
    public interface IFileTestBuilder : IBaseFileTestBuilder<IAndFileTestBuilder>
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// has the same file stream as the provided one.
        /// </summary>
        /// <param name="stream">File stream.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithStream(Stream stream);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// has the same file name as the provided one.
        /// </summary>
        /// <param name="fileName">File name as string.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithName(string fileName);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// has the same file provider as the provided one.
        /// </summary>
        /// <param name="fileProvider">File provider of type <see cref="IFileProvider"/>.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// has the same file provider type as the provided one.
        /// </summary>
        /// <typeparam name="TFileProvider">File provider of type <see cref="IFileProvider"/>.</typeparam>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithFileProviderOfType<TFileProvider>()
            where TFileProvider : IFileProvider;

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// has the same file contents as the provided byte array.
        /// </summary>
        /// <param name="fileContents">File contents as byte array.</param>
        /// <returns>The same <see cref="IAndFileTestBuilder"/>.</returns>
        IAndFileTestBuilder WithContents(byte[] fileContents);
    }
}
