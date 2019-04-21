namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>.
    /// </summary>
    public interface IViewComponentTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithStatusCodeResult<IAndViewComponentTestBuilder>,
        IBaseTestBuilderWithContentTypeResult<IAndViewComponentTestBuilder>
    {
    }
}
