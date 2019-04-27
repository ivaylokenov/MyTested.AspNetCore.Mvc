namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Json
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="JsonResult"/>.
    /// </summary>
    public interface IJsonTestBuilder : IBaseTestBuilderWithResponseModel, 
        IBaseTestBuilderWithStatusCodeResult<IAndJsonTestBuilder>,
        IBaseTestBuilderWithContentTypeResult<IAndJsonTestBuilder>,
        IBaseTestBuilderWithActionResult<JsonResult>
    {
    }
}
