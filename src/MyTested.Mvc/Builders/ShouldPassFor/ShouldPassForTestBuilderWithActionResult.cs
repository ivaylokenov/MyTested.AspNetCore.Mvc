namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using Contracts.ShouldPassFor;
    using Internal;
    using Internal.TestContexts;

    public class ShouldPassForTestBuilderWithActionResult<TActionResult> : ShouldPassForTestBuilderWithInvokedAction,
        IShouldPassForTestBuilderWithActionResult<TActionResult>
    {
        public ShouldPassForTestBuilderWithActionResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Action<TActionResult> assertions)
        {
            this.ValidateFor(assertions, this.GetActionResult());
            return this;
        }

        public IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Func<TActionResult, bool> predicate)
        {
            this.ValidateFor(predicate, this.GetActionResult());
            return this;
        }

        private TActionResult GetActionResult()
        {
            var actionResult = this.TestContext.ActionResultAs<TActionResult>();

            if (actionResult?.GetType() == typeof(VoidActionResult))
            {
                throw new InvalidOperationException("Void methods cannot provide action result because they do not have return value.");
            }

            return actionResult;
        }
    }
}
