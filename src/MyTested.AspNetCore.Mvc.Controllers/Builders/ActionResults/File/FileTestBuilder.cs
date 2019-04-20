namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.File
{
    using Contracts.ActionResults.File;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing file result.
    /// </summary>
    /// <typeparam name="TFileResult">Result of type <see cref="FileStreamResult"/>, <see cref="VirtualFileResult"/> or <see cref="FileContentResult"/>.</typeparam>
    public class FileTestBuilder<TFileResult>
        : BaseTestBuilderWithFileResult<TFileResult, IAndFileTestBuilder>, 
        IAndFileTestBuilder
        where TFileResult : FileResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTestBuilder{TFileResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public FileTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the file result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndFileTestBuilder"/> type.</value>
        public override IAndFileTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public IFileTestBuilder AndAlso() => this;
    }
}
