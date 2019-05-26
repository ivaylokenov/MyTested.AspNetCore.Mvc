namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with error <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TErrorResultTestBuilder">Type of error result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> 
        : IBaseTestBuilderWithErrorResult,
        IBaseTestBuilderWithOutputResult<TErrorResultTestBuilder>
        where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
