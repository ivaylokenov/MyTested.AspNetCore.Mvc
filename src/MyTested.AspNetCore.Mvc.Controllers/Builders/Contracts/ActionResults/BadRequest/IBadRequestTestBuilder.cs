namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.BadRequest
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="BadRequestResult"/>
    /// and <see cref="BadRequestObjectResult"/>.
    /// </summary>
    public interface IBadRequestTestBuilder 
        : IBaseTestBuilderWithOutputResult<IAndBadRequestTestBuilder>,
        IBaseTestBuilderWithActionResult<BadRequestObjectResult>
    {
    }
}