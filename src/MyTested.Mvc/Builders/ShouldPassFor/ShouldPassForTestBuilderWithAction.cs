namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using System;
    using System.Collections.Generic;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;

    public class ShouldPassForTestBuilderWithAction : ShouldPassForTestBuilderWithController<object>,
        IShouldPassForTestBuilderWithAction
    {
        public ShouldPassForTestBuilderWithAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public IShouldPassForTestBuilderWithAction TheAction(Action<string> assertions)
        {
            this.ValidateFor(assertions, this.TestContext.ActionName);
            return this;
        }

        public IShouldPassForTestBuilderWithAction TheAction(Func<string, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ActionName);
            return this;
        }

        public IShouldPassForTestBuilderWithAction TheActionAttributes(Action<IEnumerable<object>> assertions)
        {
            this.ValidateFor(assertions, this.TestContext.ActionAttributes);
            return this;
        }

        public IShouldPassForTestBuilderWithAction TheActionAttributes(Func<IEnumerable<object>, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ActionAttributes);
            return this;
        }
    }
}
