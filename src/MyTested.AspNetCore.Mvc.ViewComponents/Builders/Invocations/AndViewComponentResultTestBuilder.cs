namespace MyTested.AspNetCore.Mvc.Builders.Invocations
{
    using Contracts.Invocations;
    using Internal.TestContexts;
    using ShouldHave;

    public class AndViewComponentResultTestBuilder<TInvocationResult> : ViewComponentShouldHaveTestBuilder<TInvocationResult>,
        IAndViewComponentResultTestBuilder<TInvocationResult>
    {
        public AndViewComponentResultTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }

        public IViewComponentResultTestBuilder<TInvocationResult> AndAlso()
            => new ViewComponentResultTestBuilder<TInvocationResult>(this.TestContext);
    }
}
