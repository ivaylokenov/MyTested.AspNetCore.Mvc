namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithRouteValuesResultInternal<TRouteValuesResultTestBuilder>
        : IBaseTestBuilderWithUrlHelperResultInternal<TRouteValuesResultTestBuilder>
        where TRouteValuesResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        bool IncludeCountCheck { get; set; }
    }
}
