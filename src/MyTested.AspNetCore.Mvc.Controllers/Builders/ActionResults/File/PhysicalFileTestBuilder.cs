namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.File
{
    using Contracts.ActionResults.File;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="PhysicalFileResult"/>.
    /// </summary>
    public class PhysicalFileTestBuilder
        : BaseFileTestBuilder<PhysicalFileResult>, IAndPhysicalFileTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalFileTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public PhysicalFileTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndPhysicalFileTestBuilder WithContentType(string contentType)
        {
            this.ValidateContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndPhysicalFileTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType.Value);

        /// <inheritdoc />
        public IAndPhysicalFileTestBuilder WithFileDownloadName(string fileDownloadName)
        {
            this.ValidateFileDownloadName(fileDownloadName);
            return this;
        }

        /// <inheritdoc />
        public IAndPhysicalFileTestBuilder WithPhysicalPath(string physicalPath)
        {
            var actualPhysicalPath = (this.ActionResult as PhysicalFileResult)?.FileName;
            if (physicalPath != actualPhysicalPath)
            {
                this.ThrowNewFileResultAssertionException(
                    "FileName",
                    $"to be {physicalPath.GetErrorMessageName()}",
                    $"instead received '{actualPhysicalPath}'");
            }

            return this;
        }

        /// <inheritdoc />
        public IPhysicalFileTestBuilder AndAlso() => this;
    }
}
