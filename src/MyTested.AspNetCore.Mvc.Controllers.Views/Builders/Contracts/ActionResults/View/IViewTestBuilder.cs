namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ViewResult"/>.
    /// </summary>
    public interface IViewTestBuilder 
        : IBaseTestBuilderWithViewResult<IAndViewTestBuilder>,
        IBaseTestBuilderWithActionResult<ViewResult>
    {
    }
}
