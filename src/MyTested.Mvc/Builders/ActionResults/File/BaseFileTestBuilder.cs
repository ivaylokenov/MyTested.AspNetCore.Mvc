namespace MyTested.Mvc.Builders.ActionResults.File
{
    using System;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using MyTested.Mvc.Builders.Base;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with file action result.
    /// </summary>
    /// <typeparam name="TFileResult">Type inheriting FileResult.</typeparam>
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
        /// <param name="fileResult">Result from the tested action.</param>
        public BaseFileTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TFileResult fileResult)
            : base(controller, actionName, caughtException, fileResult)
        {
        }

        /// <summary>
        /// Gets the tested file result.
        /// </summary>
        /// <returns>Tested file result.</returns>
        public new FileResult AndProvideTheActionResult()
        {
            return this.ActionResult;
        }
        
        /// <summary>
        /// Validates whether file result has the provided content type.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        protected void ValidateContentType(MediaTypeHeaderValue contentType)
        {
            ContentTypeValidator.ValidateContentType(
                this.ActionResult,
                contentType,
                this.ThrowNewFileResultAssertionException);
        }

        /// <summary>
        /// Validates whether file result has the provided file download name.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        protected void ValidateFileDownloadName(string fileDownloadName)
        {
            var actualFileDownloadName = (this.ActionResult as FileResult)?.FileDownloadName;
            if (fileDownloadName != actualFileDownloadName)
            {
                this.ThrowNewFileResultAssertionException(
                    "FileDownloadName",
                    string.Format("to be {0}", fileDownloadName != null ? $"'{fileDownloadName}'" : "null"),
                    string.Format("instead received {0}", actualFileDownloadName != string.Empty ? $"'{actualFileDownloadName}'" : "empty string"));
            }
        }

        /// <summary>
        /// Throws new FireResultAssertionException.
        /// </summary>
        /// <param name="propertyName">Failed property name.</param>
        /// <param name="expectedValue">Expected property value.</param>
        /// <param name="actualValue">Actual property value.</param>
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
