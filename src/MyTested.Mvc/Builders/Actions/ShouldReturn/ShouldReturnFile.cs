namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.File;
    using Contracts.ActionResults.File;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="FileStreamResult"/>, <see cref="VirtualFileResult"/>, <see cref="FileContentResult"/> or <see cref="PhysicalFileResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
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

        /// <inheritdoc />
        public IPhysicalFileTestBuilder PhysicalFile()
        {
            this.TestContext.ActionResult = this.GetReturnObject<PhysicalFileResult>();
            return new PhysicalFileTestBuilder(this.TestContext);
        }

        private IFileTestBuilder ReturnFileTestBuilder<TFileResult>()
            where TFileResult : FileResult
        {
            this.TestContext.ActionResult = this.GetReturnObject<TFileResult>();
            return new FileTestBuilder<TFileResult>(this.TestContext);
        }
    }
}
