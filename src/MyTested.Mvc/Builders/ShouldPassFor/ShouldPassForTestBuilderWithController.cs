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
        private readonly ControllerTestContext testContext;

        public ShouldPassForTestBuilderWithController(ControllerTestContext testContext)
            : base(testContext)
        {
            this.testContext = testContext;
        }

        public IShouldPassForTestBuilderWithController<TController> TheController(Action<TController> assertions)
        {
            this.ValidateFor(assertions, this.testContext.ControllerAs<TController>());
            return this;
        }

        public IShouldPassForTestBuilderWithController<TController> TheController(Func<TController, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.ControllerAs<TController>());
            return this;
        }

        public IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Action<IEnumerable<object>> assertions)
        {
            this.ValidateFor(assertions, this.testContext.ControllerAttributes);
            return this;
        }

        public IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Func<IEnumerable<object>, bool> predicate)
        {
            this.ValidateFor(predicate, this.testContext.ControllerAttributes);
            return this;
        }
    }
}
