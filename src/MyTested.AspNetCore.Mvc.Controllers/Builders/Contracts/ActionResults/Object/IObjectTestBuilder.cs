namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Object
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ObjectResult"/>.
    /// </summary>
    public interface IObjectTestBuilder 
        : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithOutputResult<IAndObjectTestBuilder>,
        IBaseTestBuilderWithActionResult<ObjectResult>
    {
    }
}
