namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.NotFound
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="NotFoundResult"/>
    /// or <see cref="NotFoundObjectResult"/>.
    /// </summary>
    public interface INotFoundTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndNotFoundTestBuilder>,
        IBaseTestBuilderWithActionResult<NotFoundObjectResult>
    {
    }
}
