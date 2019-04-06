namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    public interface IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder>
        : IBaseTestBuilderWithStatusCodeResultInternal<TOutputResultTestBuilder>
        where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        ObjectResult GetObjectResult();
    }
}
