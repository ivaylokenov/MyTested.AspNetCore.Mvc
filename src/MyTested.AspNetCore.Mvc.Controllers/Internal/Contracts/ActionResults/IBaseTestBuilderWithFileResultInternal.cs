namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;

    public interface IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder>
        : IBaseTestBuilderWithActionResultInternal<TFileResultTestBuilder>
        where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
    {
    }
}
