namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.File;
    using Microsoft.AspNet.Mvc;
    using MyTested.Mvc.Builders.Contracts.ActionResults.File;

    /// <summary>
    /// Class containing methods for testing FileStreamResult, VirtualFileResult, FileContentResult or PhysicalFileResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is FileStreamResult, VirtualFileResult or FileContentResult.
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
            
            return this.ReturnFileTestBuilder<FileContentResult>();
        }

        /// <summary>
        /// Tests whether action result is PhysicalFileResult.
        /// </summary>
        /// <returns>File test builder.</returns>
        public IPhysicalFileTestBuilder PhysicalFile()
        {
            this.ValidateActionReturnType<PhysicalFileResult>();
            return new PhysicalFileTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as PhysicalFileResult);
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
