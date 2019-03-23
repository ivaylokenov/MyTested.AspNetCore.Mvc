namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Ok
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.OkResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/> result.
    /// </summary>
    public interface IOkTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndOkTestBuilder>
    {
    }
}
