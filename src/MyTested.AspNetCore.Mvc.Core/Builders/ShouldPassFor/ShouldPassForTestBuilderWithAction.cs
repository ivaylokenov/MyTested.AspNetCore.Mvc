namespace MyTested.AspNetCore.Mvc.Builders.ShouldPassFor
{
    using System;
    using System.Collections.Generic;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    public class ShouldPassForTestBuilderWithAction : ShouldPassForTestBuilderWithController<object>,
        IShouldPassForTestBuilderWithAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldPassForTestBuilderWithAction"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldPassForTestBuilderWithAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithAction TheAction(Action<string> assertions)
        {
            assertions(this.TestContext.ActionName);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithAction TheAction(Func<string, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ActionName, "action name");
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithAction TheActionAttributes(Action<IEnumerable<object>> assertions)
        {
            assertions(this.TestContext.ActionAttributes);
            return this;
        }

        /// <inheritdoc />
        public IShouldPassForTestBuilderWithAction TheActionAttributes(Func<IEnumerable<object>, bool> predicate)
        {
            this.ValidateFor(predicate, this.TestContext.ActionAttributes, "action attributes");
            return this;
        }
    }
}
