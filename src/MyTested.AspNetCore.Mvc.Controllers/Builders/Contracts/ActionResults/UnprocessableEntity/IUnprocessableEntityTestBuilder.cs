namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.UnprocessableEntity
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="UnprocessableEntityResult"/>
    /// and <see cref="UnprocessableEntityObjectResult"/>.
    /// </summary>
    public interface IUnprocessableEntityTestBuilder
        : IBaseTestBuilderWithErrorResult<IAndUnprocessableEntityTestBuilder>,
        IBaseTestBuilderWithActionResult<UnprocessableEntityObjectResult>
    {
    }
}
