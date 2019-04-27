namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.NotFound
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/>.
    /// </summary>
    public interface INotFoundTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndNotFoundTestBuilder>
    {
    }
}
