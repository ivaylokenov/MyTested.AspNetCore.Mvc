namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.StatusCode
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
    /// </summary>
    public interface IStatusCodeTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndStatusCodeTestBuilder>
    {
    }
}
