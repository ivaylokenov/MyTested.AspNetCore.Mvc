namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithViewFeatureResultInternal<TViewFeatureResultTestBuilder>
        : IBaseTestBuilderWithStatusCodeResultInternal<TViewFeatureResultTestBuilder>,
        IBaseTestBuilderWithContentTypeResultInternal<TViewFeatureResultTestBuilder>
        where TViewFeatureResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
