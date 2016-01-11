namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.File;
    using Microsoft.AspNet.Mvc;
    using MyTested.Mvc.Builders.Contracts.ActionResults.File;

    /// <summary>
    /// Class containing methods for testing FileStreamResult, VirtualFileResult, FileContentResult or PhysicalFileResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is FileStreamResult, VirtualFileResult, FileContentResult or PhysicalFileResult.
        /// </summary>
        /// <returns>File test builder.</returns>
        public IFileTestBuilder File()
        {
            if (this.ActionResult is VirtualFileResult)
            {
                return this.ReturnFileTestBuilder<VirtualFileResult>();
            }

            if (this.ActionResult is FileStreamResult)
            {
                return this.ReturnFileTestBuilder<FileStreamResult>();
            }

            if (this.ActionResult is PhysicalFileResult)
            {
                return this.ReturnFileTestBuilder<PhysicalFileResult>();
            }

            return this.ReturnFileTestBuilder<FileContentResult>();
        }

        private IFileTestBuilder ReturnFileTestBuilder<TFileResult>()
            where TFileResult : FileResult
        {
            var fileResult = this.GetReturnObject<TFileResult>();
            return new FileTestBuilder<TFileResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                fileResult);
        }
    }
}
