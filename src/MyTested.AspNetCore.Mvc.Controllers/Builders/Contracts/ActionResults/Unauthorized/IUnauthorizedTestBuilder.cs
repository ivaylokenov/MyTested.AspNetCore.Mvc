namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Unauthorized
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>	
    /// Used for testing <see cref="UnauthorizedResult"/>	
    /// or <see cref="UnauthorizedObjectResult"/> result.	
    /// </summary>	
    public interface IUnauthorizedTestBuilder
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndUnauthorizedTestBuilder>,
        IBaseTestBuilderWithActionResult<UnauthorizedObjectResult>
    {
    }
}