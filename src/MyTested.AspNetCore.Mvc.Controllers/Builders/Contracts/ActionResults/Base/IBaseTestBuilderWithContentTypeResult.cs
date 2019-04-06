namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with content type <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TContentTypeResultTestBuilder">Type of content type result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithContentTypeResult<TContentTypeResultTestBuilder> : IBaseTestBuilderWithActionResult
        where TContentTypeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
