namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>.
    /// </summary>
    public interface IViewTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithStatusCodeResult<IAndViewTestBuilder>,
        IBaseTestBuilderWithContentTypeResult<IAndViewTestBuilder>
    {
    }
}
