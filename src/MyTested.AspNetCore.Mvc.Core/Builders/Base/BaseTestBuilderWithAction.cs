namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using ShouldPassFor;
    using Utilities.Extensions;

    /// <summary>
    /// Base class for all test builders with action call.
    /// </summary>
    public abstract class BaseTestBuilderWithAction : BaseTestBuilderWithComponent, IBaseTestBuilderWithAction
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
        public string ActionName => this.TestContext.ActionName;

        /// <summary>
        /// Gets the action attributes which will be tested.
        /// </summary>
        /// <value>Action attributes to be tested.</value>
        public IEnumerable<object> ActionLevelAttributes => this.TestContext.ActionAttributes;
        
        /// <inheritdoc />
        public new IShouldPassForTestBuilderWithAction ShouldPassFor() => new ShouldPassForTestBuilderWithAction(this.TestContext);

        /// <summary>
        /// Tests whether the tested action's model state is valid.
        /// </summary>
        public void CheckValidModelState()
        {
            if (!this.TestContext.ModelState.IsValid)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have valid model state with no errors, but it had some.",
                    this.ActionName,
                    this.Component.GetName()));
            }
        }
    }
}
