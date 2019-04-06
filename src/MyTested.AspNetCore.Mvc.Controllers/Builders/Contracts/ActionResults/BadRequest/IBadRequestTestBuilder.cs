namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.BadRequest
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
    /// and <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>.
    /// </summary>
    public interface IBadRequestTestBuilder : IBaseTestBuilderWithOutputResult<IAndBadRequestTestBuilder>
    {
    }
}
