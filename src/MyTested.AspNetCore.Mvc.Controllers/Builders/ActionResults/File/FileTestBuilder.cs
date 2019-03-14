namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.File
{
    using System.IO;
    using System.Linq;
    using Contracts.ActionResults.File;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing file result.
    /// </summary>
    /// <typeparam name="TFileResult">Result of type <see cref="FileStreamResult"/>, <see cref="VirtualFileResult"/> or <see cref="FileContentResult"/>.</typeparam>
    public class FileTestBuilder<TFileResult>
        : BaseFileTestBuilder<TFileResult>, IAndFileTestBuilder
        where TFileResult : FileResult
    {
        private const string FileName = "file name";
        private const string FileProvider = "file provider";
        private const string FileStream = "file stream";
        private const string FileContents = "file contents";

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTestBuilder{TFileResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public FileTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithContentType(string contentType)
        {
            this.ValidateContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType.Value);

        /// <inheritdoc />
        public IAndFileTestBuilder WithDownloadName(string fileDownloadName)
        {
            this.ValidateFileDownloadName(fileDownloadName);
            return this;
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithStream(Stream stream)
        {
            var fileStreamResult = this.GetFileResult<FileStreamResult>(FileStream);
            var expectedContents = stream.ToByteArray();
            var actualContents = fileStreamResult.FileStream.ToByteArray();
            if (!expectedContents.SequenceEqual(actualContents))
            {
                this.ThrowNewFileResultAssertionException(
                    nameof(fileStreamResult.FileStream),
                    "to have contents as the provided one",
                    "instead received different result");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithName(string fileName)
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileName);
            var actualFileName = virtualFileResult.FileName;
            if (fileName != virtualFileResult.FileName)
            {
                this.ThrowNewFileResultAssertionException(
                    nameof(virtualFileResult.FileName),
                    $"to be '{fileName}'",
                    $"instead received '{actualFileName}'");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider)
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileProvider);
            if (fileProvider != virtualFileResult.FileProvider)
            {
                this.ThrowNewFileResultAssertionException(
                    nameof(virtualFileResult.FileProvider),
                    "to be the same as the provided one",
                    "instead received different result");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithFileProviderOfType<TFileProvider>()
            where TFileProvider : IFileProvider
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileProvider);
            var actualFileProvider = virtualFileResult.FileProvider;

            if (actualFileProvider == null ||
                Reflection.AreDifferentTypes(typeof(TFileProvider), actualFileProvider.GetType()))
            {
                this.ThrowNewFileResultAssertionException(
                    nameof(virtualFileResult.FileProvider),
                    $"to be of {typeof(TFileProvider).Name} type",
                    $"instead received {actualFileProvider.GetName()}");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndFileTestBuilder WithContents(byte[] fileContents)
        {
            var fileContentResult = this.GetFileResult<FileContentResult>(FileContents);
            if (!fileContents.SequenceEqual(fileContentResult.FileContents))
            {
                this.ThrowNewFileResultAssertionException(
                    nameof(fileContentResult.FileContents),
                   "to have contents as the provided ones",
                   "instead received different result");
            }

            return this;
        }

        /// <inheritdoc />
        public IFileTestBuilder AndAlso() => this;
        
        private TExpectedFileResult GetFileResult<TExpectedFileResult>(string containment)
            where TExpectedFileResult : class
        {
            var actualFileResult = this.ActionResult as TExpectedFileResult;
            if (actualFileResult == null)
            {
                this.ThrowNewFileResultAssertionException(
                    "to contain",
                    containment,
                    "it could not be found");
            }

            return actualFileResult;
        }
    }
}
