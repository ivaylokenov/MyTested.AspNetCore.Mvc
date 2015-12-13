namespace MyTested.Mvc.Builders.ActionResults.File
{
    using Base;
    using Contracts.ActionResults.File;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.IO;
    public class FileTestBuilder<TFileResult>
        : BaseTestBuilderWithActionResult<TFileResult>, IFileTestBuilder
        where TFileResult : FileResult
    {
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
            return null;
        }

        public IAndFileTestBuilder WithFileName(string fileName)
        {
            return null;
        }

        public IAndFileTestBuilder WithFileProvider(IFileProvider fileProvider)
        {
            return null;
        }

        public IAndFileTestBuilder WithFileContents(byte[] fileContents)
        {
            return null;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining file result tests.
        /// </summary>
        /// <returns>File result test builder.</returns>
        public IFileTestBuilder AndAlso()
        {
            return this;
        }
    }
}
