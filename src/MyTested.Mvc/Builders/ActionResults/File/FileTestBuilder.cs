namespace MyTested.Mvc.Builders.ActionResults.File
{
    using Base;
    using Contracts.ActionResults.File;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using System;
    using System.IO;
    using System.Linq;

    public class FileTestBuilder<TFileResult>
        : BaseTestBuilderWithActionResult<TFileResult>, IFileTestBuilder
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
        /// <param name="actionResult">Result from the tested action.</param>
        public FileTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TFileResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IAndFileTestBuilder WithContentType(string contentType)
        {
            return this.WithContentType(new MediaTypeHeaderValue(contentType));
        }

        public IAndFileTestBuilder WithContentType(MediaTypeHeaderValue contentType)
        {
            var actualContentType = this.ActionResult.ContentType;
            if (contentType != actualContentType)
            {
                // TODO: exception
            }

            return this;
        }

        public IAndFileTestBuilder WithFileDownloadName(string fileDownloadName)
        {
            var actualFileDownloadName = this.ActionResult.FileDownloadName;
            if (fileDownloadName != actualFileDownloadName)
            {
                // TODO: exception
            }

            return this;
        }

        public IAndFileTestBuilder WithFileStream(Stream stream)
        {
            var fileStreamResult = this.GetFileResult<FileStreamResult>(FileStream);
            var expectedContents = this.GetByteArrayFromStream(stream);
            var actualContents = this.GetByteArrayFromStream(fileStreamResult.FileStream);
            if (!expectedContents.SequenceEqual(actualContents))
            {
                // TODO: exception
            }

            return this;
        }

        public IAndFileTestBuilder WithFileName(string fileName)
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileName);
            if (fileName != virtualFileResult.FileName)
            {
                // TODO: exception
            }

            return this;
        }

        public IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider)
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileProvider);
            if (fileProvider != virtualFileResult.FileProvider)
            {
                // TODO: exception
            }

            return this;
        }

        public IAndFileTestBuilder WithFileProviderOfType<TFileProvider>()
            where TFileProvider : IFileProvider
        {
            var virtualFileResult = this.GetFileResult<VirtualFileResult>(FileProvider);
            if (Reflection.AreDifferentTypes(typeof(TFileProvider), virtualFileResult.FileProvider.GetType()))
            {
                // TODO: exception
            }

            return this;
        }

        public IAndFileTestBuilder WithFileContents(byte[] fileContents)
        {
            var fileContentResult = this.GetFileResult<FileContentResult>(FileContents);
            if (!fileContents.SequenceEqual(fileContentResult.FileContents))
            {
                // TODO: exception
            }

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining file result tests.
        /// </summary>
        /// <returns>File result test builder.</returns>
        public IFileTestBuilder AndAlso()
        {
            return this;
        }

        private byte[] GetByteArrayFromStream(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private TExpectedFileResult GetFileResult<TExpectedFileResult>(string containment)
            where TExpectedFileResult : class
        {
            var actualFileResult = this.ActionResult as TExpectedFileResult;
            if (actualFileResult == null)
            {
                // TODO: fix
                //throw new RedirectResultAssertionException(string.Format(
                //    "When calling {0} action in {1} expected redirect result to contain {2}, but it could not be found.",
                //    this.ActionName,
                //    this.Controller.GetName(),
                //    containment));
            }

            return actualFileResult;
        }
    }
}
