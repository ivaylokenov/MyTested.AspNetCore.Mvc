namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Object
{
    using Base;
    using Contracts.Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/>.
    /// </summary>
    public interface IObjectTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndObjectTestBuilder>
    {
    }
}
