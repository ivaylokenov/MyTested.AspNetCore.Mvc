namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Internal.TestContexts;

    /// <summary>
    /// Base class for all test builders with action call.
    /// </summary>
    public abstract class BaseTestBuilderWithAction : BaseTestBuilderWithController, IBaseTestBuilderWithAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithAction"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        public string ActionName => this.TestContext.MethodName;

        /// <summary>
        /// Gets the action attributes which will be tested.
        /// </summary>
        /// <value>Action attributes to be tested.</value>
        public IEnumerable<object> ActionAttributes => this.TestContext.MethodAttributes;
    }
}
