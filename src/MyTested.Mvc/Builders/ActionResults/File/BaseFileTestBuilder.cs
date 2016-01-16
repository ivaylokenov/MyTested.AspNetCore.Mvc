namespace MyTested.Mvc.Builders.ActionResults.File
{
    using System;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using MyTested.Mvc.Builders.Base;

    /// <summary>
    /// Base class for all test builders with file action result.
    /// </summary>
    public abstract class BaseFileTestBuilder<TFileResult> :
        BaseTestBuilderWithActionResult<TFileResult>
        where TFileResult : FileResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFileTestBuilder{TFileResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public BaseFileTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TFileResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        protected void ValidateContentType(MediaTypeHeaderValue contentType)
        {
            var expectedContentType = contentType?.MediaType;
            var actualContentType = this.ActionResult.ContentType?.MediaType;
            if (expectedContentType != actualContentType)
            {
                this.ThrowNewFileResultAssertionException(
                    "ContentType",
                    string.Format("to be {0}", contentType != null ? contentType.MediaType : "null"),
                    string.Format("instead received {0}", actualContentType != null ? actualContentType : "null"));
            }
        }

        protected void ValidateFileDownloadName(string fileDownloadName)
        {
            var actualFileDownloadName = this.ActionResult.FileDownloadName;
            if (fileDownloadName != actualFileDownloadName)
            {
                this.ThrowNewFileResultAssertionException(
                    "FileDownloadName",
                    string.Format("to be '{0}'", fileDownloadName != null ? fileDownloadName : "null"),
                    string.Format("instead received '{0}'", actualFileDownloadName != null ? actualFileDownloadName : "null"));
            }
        }

        protected void ThrowNewFileResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new FileResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected file result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
