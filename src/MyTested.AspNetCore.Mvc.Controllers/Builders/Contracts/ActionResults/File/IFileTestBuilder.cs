namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    using System;
    using And;
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="FileResult"/>,
    /// <see cref="FileContentResult"/>, <see cref="FileStreamResult"/>
    /// or <see cref="VirtualFileResult"/>.
    /// </summary>
    public interface IFileTestBuilder 
        : IBaseTestBuilderWithFileResult<IAndFileTestBuilder>,
        IBaseTestBuilderWithActionResult<FileContentResult>,
        IBaseTestBuilderWithActionResult<FileStreamResult>
    {
        /// <summary>
        /// Tests whether the <see cref="VirtualFileResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="VirtualFileResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Action<VirtualFileResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="VirtualFileResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="VirtualFileResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Func<VirtualFileResult, bool> predicate);
    }
}
