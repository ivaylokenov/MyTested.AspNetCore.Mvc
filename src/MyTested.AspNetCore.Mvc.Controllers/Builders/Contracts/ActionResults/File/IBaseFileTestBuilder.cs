namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.File
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>.
    /// </summary>
    /// <typeparam name="TFileResultTestBuilder">Type of file result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseFileTestBuilder<TFileResultTestBuilder> : IBaseTestBuilderWithContentTypeResult<TFileResultTestBuilder>
        where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>
        /// has the same file download name as the provided one.
        /// </summary>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.FileResult"/> test builder.</returns>
        TFileResultTestBuilder WithDownloadName(string fileDownloadName);
    }
}
