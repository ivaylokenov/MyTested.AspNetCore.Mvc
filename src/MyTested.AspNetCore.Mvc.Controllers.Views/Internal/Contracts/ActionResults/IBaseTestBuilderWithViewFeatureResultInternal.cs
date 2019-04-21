namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithViewFeatureResultInternal<TViewFeatureResultTestBuilder>
        : IBaseTestBuilderWithActionResultInternal<TViewFeatureResultTestBuilder>
        where TViewFeatureResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
