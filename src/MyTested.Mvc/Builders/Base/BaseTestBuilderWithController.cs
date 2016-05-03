namespace MyTested.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Internal.TestContexts;
    using ShouldPassFor;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with controller.
    /// </summary>
    public abstract class BaseTestBuilderWithController : BaseTestBuilder, IBaseTestBuilderWithController
    {
        private ControllerTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithController" /> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithController(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets the controller on which the action will be tested.
        /// </summary>
        /// <value>Controller on which the action will be tested.</value>
        internal object Controller => this.TestContext.Controller;

        /// <summary>
        /// Gets the controller attributes which will be tested.
        /// </summary>
        /// <value>Controller attributes which will be tested.</value>
        internal IEnumerable<object> ControllerLevelAttributes => this.TestContext.ControllerAttributes;

        /// <summary>
        /// Gets the currently used <see cref="ControllerTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ControllerTestContext"/>.</value>
        protected new ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value.Controller, nameof(this.Controller));
                this.testContext = value;
            }
        }

        /// <inheritdoc />
        public new IShouldPassForTestBuilderWithController<object> ShouldPassFor() 
            => new ShouldPassForTestBuilderWithController<object>(this.TestContext);
    }
}
