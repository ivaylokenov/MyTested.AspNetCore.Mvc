namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;

    public class BaseTestBuilderWithViewComponentResult<TInvocationResult> : BaseTestBuilderWithViewComponentInvocation,
        IBaseTestBuilderWithViewComponentResult<TInvocationResult>
    {
        public BaseTestBuilderWithViewComponentResult(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
