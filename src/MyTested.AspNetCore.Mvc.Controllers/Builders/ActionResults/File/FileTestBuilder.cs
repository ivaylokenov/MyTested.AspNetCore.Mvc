namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.File
{
    using System;
    using Contracts.ActionResults.File;
    using Contracts.And;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing file result.
    /// </summary>
    /// <typeparam name="TFileResult">
    /// Result of type <see cref="FileStreamResult"/>,
    /// <see cref="VirtualFileResult"/>
    /// or <see cref="FileContentResult"/>.
    /// </typeparam>
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
        public IAndTestBuilder Passing(Action<FileContentResult> assertions)
        {
            this.ValidateFileResult<FileContentResult>();
            return this.Passing<FileContentResult>(assertions);
        }

        public IAndTestBuilder Passing(Func<FileContentResult, bool> predicate)
        {
            this.ValidateFileResult<FileContentResult>();
            return this.Passing<FileContentResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<FileStreamResult> assertions)
        {
            this.ValidateFileResult<FileStreamResult>();
            return this.Passing<FileStreamResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<FileStreamResult, bool> predicate)
        {
            this.ValidateFileResult<FileStreamResult>();
            return this.Passing<FileStreamResult>(predicate);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Action<VirtualFileResult> assertions)
        {
            this.ValidateFileResult<VirtualFileResult>();
            return this.Passing<VirtualFileResult>(assertions);
        }

        /// <inheritdoc />
        public IAndTestBuilder Passing(Func<VirtualFileResult, bool> predicate)
        {
            this.ValidateFileResult<VirtualFileResult>();
            return this.Passing<VirtualFileResult>(predicate);
        }

        /// <inheritdoc />
        public IFileTestBuilder AndAlso() => this;
        
        private void ValidateFileResult<TResult>()
            where TResult : FileResult
        {
            var actualResultType = this.ActionResult.GetType();
            var expectedResultType = typeof(TResult);

            if (actualResultType != expectedResultType)
            {
                throw new FileResultAssertionException(string.Format(
                    "{0} file result to be {1}, but it was {2}.",
                    this.TestContext.ExceptionMessagePrefix,
                    expectedResultType,
                    actualResultType));
            }
        }
    }
}
