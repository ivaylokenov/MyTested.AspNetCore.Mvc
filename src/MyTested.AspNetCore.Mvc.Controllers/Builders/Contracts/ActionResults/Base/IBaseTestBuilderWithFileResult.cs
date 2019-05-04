namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base interface for all test builders with <see cref="FileResult"/>.
    /// </summary>
    /// <typeparam name="TFileResultTestBuilder">Type of file result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithFileResult<TFileResultTestBuilder> 
        : IBaseTestBuilderWithContentTypeResult<TFileResultTestBuilder>,
        IBaseTestBuilderWithActionResult<FileResult> 
        where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
