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
            assertions(this.TestContext.ActionName);
            return this;
        }

        public IShouldPassForTestBuilderWithAction TheAction(Func<string, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ActionName, "action name");
            return this;
        }

        public IShouldPassForTestBuilderWithAction TheActionAttributes(Action<IEnumerable<object>> assertions)
        {
            assertions(this.TestContext.ActionAttributes);
            return this;
        }

        public IShouldPassForTestBuilderWithAction TheActionAttributes(Func<IEnumerable<object>, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ActionAttributes, "action attributes");
            return this;
        }
    }
}
