namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ViewComponentResult"/>.
    /// </summary>
    public interface IViewComponentTestBuilder 
        : IBaseTestBuilderWithViewResult<IAndViewComponentTestBuilder>,
        IBaseTestBuilderWithActionResult<ViewComponentResult>
    {
    }
}
