namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;

    /// <summary>
    /// Base interface for all test builders with <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>.
    /// </summary>
    /// <typeparam name="TViewResultTestBuilder">Type of view result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithViewResult<TViewResultTestBuilder> 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithStatusCodeResult<TViewResultTestBuilder>,
        IBaseTestBuilderWithContentTypeResult<TViewResultTestBuilder>
        where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
