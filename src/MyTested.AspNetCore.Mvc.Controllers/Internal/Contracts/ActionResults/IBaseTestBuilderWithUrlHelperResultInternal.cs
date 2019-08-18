namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithUrlHelperResultInternal<TUrlHelperResultTestBuilder>
        : IBaseTestBuilderWithActionResultInternal<TUrlHelperResultTestBuilder>
        where TUrlHelperResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
