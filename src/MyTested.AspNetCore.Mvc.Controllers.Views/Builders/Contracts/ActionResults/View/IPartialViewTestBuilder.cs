namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="PartialViewResult"/>.
    /// </summary>
    public interface IPartialViewTestBuilder
        : IBaseTestBuilderWithViewResult<IAndPartialViewTestBuilder>,
        IBaseTestBuilderWithActionResult<PartialViewResult>
    {
    }
}
