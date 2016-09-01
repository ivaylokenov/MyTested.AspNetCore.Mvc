using MyTested.AspNetCore.Mvc.Internal.TestContexts;

namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    public class BaseTestBuilderWithViewComponentResult<TInvocationResult> : BaseTestBuilderWithViewComponentInvocation
    {
        public BaseTestBuilderWithViewComponentResult(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
