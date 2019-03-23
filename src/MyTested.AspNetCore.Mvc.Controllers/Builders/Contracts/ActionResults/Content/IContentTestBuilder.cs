namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Content
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
    /// </summary>
    public interface IContentTestBuilder : IBaseTestBuilderWithStatusCodeResult<IAndContentTestBuilder>,
        IBaseTestBuilderWithContentTypeResult<IAndContentTestBuilder>
    {
    }
}
