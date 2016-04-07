namespace MyTested.Mvc.Builders.ShouldPassFor
{
    using Internal.TestContexts;
    using Contracts.ShouldPassFor;
    using System;
    using System.Collections.Generic;

    public class ShouldPassForTestBuilderWithController<TController> : ShouldPassForTestBuilder,
        IShouldPassForTestBuilderWithController<TController>
        where TController : class
    {
        public ShouldPassForTestBuilderWithController(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

        protected ControllerTestContext TestContext { get; private set; }

        public IShouldPassForTestBuilderWithController<TController> TheController(Action<TController> assertions)
        {
            this.ValidateFor(assertions, this.TestContext.ControllerAs<TController>());
            return this;
        }

        public IShouldPassForTestBuilderWithController<TController> TheController(Func<TController, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ControllerAs<TController>());
            return this;
        }

        public IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Action<IEnumerable<object>> assertions)
        {
            this.ValidateFor(assertions, this.TestContext.ControllerAttributes);
            return this;
        }

        public IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Func<IEnumerable<object>, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ControllerAttributes);
            return this;
        }
    }
}
