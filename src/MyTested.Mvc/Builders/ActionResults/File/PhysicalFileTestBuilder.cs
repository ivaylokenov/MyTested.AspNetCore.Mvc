namespace MyTested.Mvc.Builders.ActionResults.File
{
    using System;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using MyTested.Mvc.Builders.Contracts.ActionResults.File;

    /// <summary>
    /// Used for testing physical file result.
    /// </summary>
    public class PhysicalFileTestBuilder
        : BaseFileTestBuilder<PhysicalFileResult>, IAndPhysicalFileTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalFileTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="physicalFileResult">Result from the tested action.</param>
        public PhysicalFileTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            PhysicalFileResult physicalFileResult)
            : base(controller, actionName, caughtException, physicalFileResult)
        {
        }
        
        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same file test builder.</returns>
        public IAndPhysicalFileTestBuilder WithContentType(string contentType)
        {
            return this.WithContentType(new MediaTypeHeaderValue(contentType));
        }

        /// <summary>
        /// Tests whether file result has the same content type as the provided one.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same file test builder.</returns>
        public IAndPhysicalFileTestBuilder WithContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether file result has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same file test builder.</returns>
        public IAndPhysicalFileTestBuilder WithFileDownloadName(string fileDownloadName)
        {
            this.ValidateFileDownloadName(fileDownloadName);
            return this;
        }

        /// <summary>
        /// Tests whether file result has the same physical file path as the provided one.
        /// </summary>
        /// <param name="physicalPath">File physical path as string.</param>
        /// <returns>The same file test builder.</returns>
        public IAndPhysicalFileTestBuilder WithPhysicalPath(string physicalPath)
        {
            var actualPhysicalPath = this.ActionResult.FileName;
            if (physicalPath != actualPhysicalPath)
            {
                this.ThrowNewFileResultAssertionException(
                    "FileName",
                    string.Format("to be '{0}'", physicalPath != null ? physicalPath : "null"),
                    string.Format("instead received '{0}'", actualPhysicalPath != null ? actualPhysicalPath : "null"));
            }

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining physical file result tests.
        /// </summary>
        /// <returns>Physical file result test builder.</returns>
        public IPhysicalFileTestBuilder AndAlso()
        {
            return this;
        }
    }
}
