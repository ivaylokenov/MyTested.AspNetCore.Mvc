namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>.
    /// </summary>
    /// <typeparam name="TFileResultTestBuilder">Type of file result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithFileResult<TFileResultTestBuilder> 
        : IBaseTestBuilderWithContentTypeResult<TFileResultTestBuilder>
        where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
