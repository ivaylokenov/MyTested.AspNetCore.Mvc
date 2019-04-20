namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;
    using TestContexts;

    public interface IBaseTestBuilderWithAuthenticationResultInternal<TAuthenticationResultTestBuilder>
        : IBaseTestBuilderWithActionResultInternal<TAuthenticationResultTestBuilder>
        where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        new ControllerTestContext TestContext { get; }
    }
}
