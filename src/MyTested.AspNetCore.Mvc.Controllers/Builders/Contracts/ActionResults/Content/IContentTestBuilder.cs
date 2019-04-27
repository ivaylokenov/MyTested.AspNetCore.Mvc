namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Content
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ContentResult"/>.
    /// </summary>
    public interface IContentTestBuilder 
        : IBaseTestBuilderWithStatusCodeResult<IAndContentTestBuilder>,
        IBaseTestBuilderWithContentTypeResult<IAndContentTestBuilder>,
        IBaseTestBuilderWithActionResult<ContentResult>
    {
    }
}
