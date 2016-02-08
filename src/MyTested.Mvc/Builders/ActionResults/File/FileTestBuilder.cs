namespace MyTested.Mvc.Builders.ActionResults.File
{
    using System;
    using System.IO;
    using System.Linq;
    using Contracts.ActionResults.File;
    using Exceptions;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Microsoft.Extensions.FileProviders;
    /// <summary>
    /// Used for testing file result.
    /// </summary>
    /// <typeparam name="TFileResult">Result of type FileStreamResult, VirtualFileResult or FileContentResult.</typeparam>
    public class FileTestBuilder<TFileResult>
        : BaseFileTestBuilder<TFileResult>, IAndFileTestBuilder
        where TFileResult : FileResult
    {
        private const string FileName = "file name";
        private const string FileProvider = "file provider";
        private const string FileStream = "file stream";
        private const string FileContents = "file contents";

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTestBuilder{TFileResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="fileResult">Result from the tested action.</param>
        public FileTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TFileResult fileResult)
            : base(controller, actionName, caughtException, fileResult)
        {
        }

        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithContentType(string contentType)
        {
            this.ValidateContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType);

        /// <summary>
        /// Tests whether file result has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithFileDownloadName(string fileDownloadName)
        {
            this.ValidateFileDownloadName(fileDownloadName);
            return this;
        }

        /// <summary>
        /// Tests whether file result has the same file stream as the provided one.
        /// </summary>
        /// <param name="stream">File stream.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithStream(Stream stream)
        {
            var fileStreamResult = this.GetFileResult<FileStreamResult>(FileStream);
            var expectedContents = stream.ToByteArray();
            var actualContents = fileStreamResult.FileStream.ToByteArray();
            if (!expectedContents.SequenceEqual(actualContents))
            {
                this.ThrowNewFileResultAssertionException(
                    "FileStream",
                    "to have contents as the provided one",
                    "instead received different result");
            }

            return this;
        }

        /// <summary>
        /// Tests whether file result has the same file name as the provided one.
        /// </summary>
        /// <param name="fileName">File name as string.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithFileName(string fileName)
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileName);
            var actualFileName = virtualFileResult.FileName;
            if (fileName != virtualFileResult.FileName)
            {
                this.ThrowNewFileResultAssertionException(
                    "FileName",
                    $"to be '{fileName}'",
                    $"instead received '{actualFileName}'");
            }

            return this;
        }

        /// <summary>
        /// Tests whether file result has the same file provider as the provided one.
        /// </summary>
        /// <param name="fileProvider">File provider of type IFileProvider.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider)
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileProvider);
            if (fileProvider != virtualFileResult.FileProvider)
            {
                this.ThrowNewFileResultAssertionException(
                    "FileProvider",
                    "to be the same as the provided one",
                    "instead received different result");
            }

            return this;
        }

        /// <summary>
        /// Tests whether file result has the same file provider type as the provided one.
        /// </summary>
        /// <typeparam name="TFileProvider">File provider of type IFileProvider.</typeparam>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithFileProviderOfType<TFileProvider>()
            where TFileProvider : IFileProvider
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileProvider);
            var actualFileProvider = virtualFileResult.FileProvider;

            if (actualFileProvider == null ||
                Reflection.AreDifferentTypes(typeof(TFileProvider), actualFileProvider.GetType()))
            {
                this.ThrowNewFileResultAssertionException(
                    "FileProvider",
                    $"to be of {typeof(TFileProvider).Name} type",
                    $"instead received {actualFileProvider.GetName()}");
            }

            return this;
        }

        /// <summary>
        /// Tests whether file result has the same file contents as the provided byte array.
        /// </summary>
        /// <param name="fileContents">File contents as byte array.</param>
        /// <returns>The same file test builder.</returns>
        public IAndFileTestBuilder WithContents(byte[] fileContents)
        {
            var fileContentResult = this.GetFileResult<FileContentResult>(FileContents);
            if (!fileContents.SequenceEqual(fileContentResult.FileContents))
            {
                this.ThrowNewFileResultAssertionException(
                   "FileContents",
                   "to have contents as the provided ones",
                   "instead received different result");
            }

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining file result tests.
        /// </summary>
        /// <returns>File result test builder.</returns>
        public IFileTestBuilder AndAlso() => this;
        
        private TExpectedFileResult GetFileResult<TExpectedFileResult>(string containment)
            where TExpectedFileResult : class
        {
            var actualFileResult = this.ActionResult as TExpectedFileResult;
            if (actualFileResult == null)
            {      
                throw new FileResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected file result to contain {2}, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    containment));
            }

            return actualFileResult;
        }
    }
}
