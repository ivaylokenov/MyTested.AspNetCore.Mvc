namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Created
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/>.
    /// </summary>
    public interface ICreatedTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndCreatedTestBuilder>, 
        IBaseTestBuilderWithRouteValuesResult<IAndCreatedTestBuilder>
    {
    }
}
