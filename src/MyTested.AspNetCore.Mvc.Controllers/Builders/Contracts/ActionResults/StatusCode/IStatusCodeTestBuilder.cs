namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.StatusCode
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="StatusCodeResult"/>
    /// or <see cref="ObjectResult"/>.
    /// </summary>
    public interface IStatusCodeTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndStatusCodeTestBuilder>,
        IBaseTestBuilderWithActionResult<StatusCodeResult>,
        IBaseTestBuilderWithActionResult<ObjectResult>
    {
    }
}
