namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithRedirectResultInternal<TRedirectResultTestBuilder>
        : IBaseTestBuilderWithUrlHelperResultInternal<TRedirectResultTestBuilder>
        where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
