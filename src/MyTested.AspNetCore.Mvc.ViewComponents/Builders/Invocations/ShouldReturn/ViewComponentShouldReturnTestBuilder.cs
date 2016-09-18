namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using And;
    using Base;
    using Contracts.Base;
    using Contracts.Invocations;
    using Internal.TestContexts;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult> : BaseTestBuilderWithViewComponentResult<TInvocationResult>,
        IViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        public ViewComponentShouldReturnTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
