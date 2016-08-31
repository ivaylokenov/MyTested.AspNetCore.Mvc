namespace MyTested.AspNetCore.Mvc.Builders.Invocations
{
    using Base;
    using Contracts.Invocations;
    using Internal.TestContexts;

    public class ViewComponentResultTestBuilder<TInvocationResult> : BaseTestBuilderWithViewComponent, 
        IViewComponentResultTestBuilder<TInvocationResult>
    {
        public ViewComponentResultTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
