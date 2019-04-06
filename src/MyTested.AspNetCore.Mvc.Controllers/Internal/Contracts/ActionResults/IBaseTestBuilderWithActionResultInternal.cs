namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;
    using TestContexts;

    public interface IBaseTestBuilderWithActionResultInternal<TResultTestBuilder>
        where TResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        ComponentTestContext TestContext { get; }

        TResultTestBuilder ResultTestBuilder { get; }
    }
}
