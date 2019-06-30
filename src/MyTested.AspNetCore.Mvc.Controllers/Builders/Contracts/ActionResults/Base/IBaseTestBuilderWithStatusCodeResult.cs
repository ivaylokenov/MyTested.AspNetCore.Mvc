namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with status code action result.
    /// </summary>
    /// <typeparam name="TStatusCodeResultTestBuilder">Type of status code result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithStatusCodeResult<TStatusCodeResultTestBuilder> 
        : IBaseTestBuilderWithActionResult
        where TStatusCodeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
