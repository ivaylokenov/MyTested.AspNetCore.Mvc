namespace MyTested.Mvc.Builders.Base
{
    using System;
    using And;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using ShouldPassFor;

    /// <summary>
    /// Base class for test builders with invoked action.
    /// </summary>
    public abstract class BaseTestBuilderWithInvokedAction
        : BaseTestBuilderWithAction, IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithInvokedAction"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithInvokedAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the caught exception. Returns null, if such does not exist.
        /// </summary>
        /// <value>Result of type <see cref="Exception"/>.</value>
        internal Exception CaughtException => this.TestContext.CaughtException;
        
        /// <inheritdoc />
        public new IShouldPassForTestBuilderWithInvokedAction ShouldPassFor()
            => new ShouldPassForTestBuilderWithInvokedAction(this.TestContext);

        /// <summary>
        /// Creates new <see cref="AndProvideTestBuilder"/>.
        /// </summary>
        /// <returns>Test builder of type <see cref="IBaseTestBuilderWithInvokedAction"/>.</returns>
        public IBaseTestBuilderWithInvokedAction NewAndProvideTestBuilder()
            => new AndProvideTestBuilder(this.TestContext);
    }
}
