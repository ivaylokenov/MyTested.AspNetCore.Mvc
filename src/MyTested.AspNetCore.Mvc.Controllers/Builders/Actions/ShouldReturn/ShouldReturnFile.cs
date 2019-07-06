namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.File;
    using Contracts.ActionResults.File;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="FileStreamResult"/>, <see cref="VirtualFileResult"/>,
    /// <see cref="FileContentResult"/> or <see cref="PhysicalFileResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder File() => this.File(null);

        /// <inheritdoc />
        public IAndTestBuilder File(Action<IFileTestBuilder> fileTestBuilder)
        {
            if (this.ActionResult is VirtualFileResult)
            {
                return this.ValidateFileResult<VirtualFileResult>(fileTestBuilder);
            }

            if (this.ActionResult is FileStreamResult)
            {
                return this.ValidateFileResult<FileStreamResult>(fileTestBuilder);
            }
            
            return this.ValidateFileResult<FileContentResult>(fileTestBuilder);
        }

        /// <inheritdoc />
        public IAndTestBuilder PhysicalFile() => this.PhysicalFile(null);

        /// <inheritdoc />
        public IAndTestBuilder PhysicalFile(Action<IPhysicalFileTestBuilder> physicalFileTestBuilder)
            => this.ValidateActionResult<PhysicalFileResult, IPhysicalFileTestBuilder>(
                physicalFileTestBuilder,
                new PhysicalFileTestBuilder(this.TestContext));

        private IAndTestBuilder ValidateFileResult<TFileResult>(Action<IFileTestBuilder> fileTestBuilder)
            where TFileResult : FileResult
            => this.ValidateActionResult<TFileResult, IFileTestBuilder>(
                fileTestBuilder,
                new FileTestBuilder<TFileResult>(this.TestContext));
    }
}
