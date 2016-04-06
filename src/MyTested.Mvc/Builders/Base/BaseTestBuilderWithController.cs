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
        /// <param name="testContext"></param>
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

        internal IEnumerable<object> ControllerLevelAttributes => this.TestContext.ControllerAttributes;

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

        public new IShouldPassForTestBuilderWithController<object> ShouldPassFor()
        {
            return new ShouldPassForTestBuilderWithController<object>(this.TestContext);
        }
    }
}
