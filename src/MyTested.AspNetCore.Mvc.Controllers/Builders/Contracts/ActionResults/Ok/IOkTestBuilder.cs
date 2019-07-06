namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Ok
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="OkResult"/>
    /// or <see cref="OkObjectResult"/> result.
    /// </summary>
    public interface IOkTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndOkTestBuilder>,
        IBaseTestBuilderWithActionResult<OkObjectResult>
    {
    }
}
