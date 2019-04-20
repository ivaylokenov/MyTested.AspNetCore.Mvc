namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithRedirectResultInternal<TRedirectResultTestBuilder>
        : IBaseTestBuilderWithActionResultInternal<TRedirectResultTestBuilder>
        where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
