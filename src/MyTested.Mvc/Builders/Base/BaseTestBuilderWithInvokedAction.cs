namespace MyTested.Mvc.Builders.Base
{
    using System;
    using And;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using ShouldPassFor;

    /// <summary>
    /// Base class for test builders with caught exception.
    /// </summary>
    public abstract class BaseTestBuilderWithInvokedAction
        : BaseTestBuilderWithAction, IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithInvokedAction" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        internal Exception CaughtException => this.TestContext.CaughtException;
        
        public new IShouldPassForTestBuilderWithInvokedAction ShouldPassFor()
            => new ShouldPassForTestBuilderWithInvokedAction(this.TestContext);

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder.</returns>
        protected IBaseTestBuilderWithInvokedAction NewAndProvideTestBuilder()
            => new AndProvideTestBuilder(this.TestContext);
    }
}
