namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with component.
    /// </summary>
    public abstract class BaseTestBuilderWithController : BaseTestBuilderWithComponent, IBaseTestBuilderWithController
    {
        private ControllerTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithController"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithController(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets the component which will be tested.
        /// </summary>
        /// <value>Component which will be tested.</value>
        public object Controller => this.TestContext.Component;

        /// <summary>
        /// Gets the component attributes which will be tested.
        /// </summary>
        /// <value>Component attributes which will be tested.</value>
        public IEnumerable<object> ControllerLevelAttributes => this.TestContext.ComponentAttributes;

        /// <summary>
        /// Gets the currently used <see cref="ControllerTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ControllerTestContext"/>.</value>
        public new ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value.Component, nameof(this.Controller));
                this.testContext = value;
            }
        }
    }
}
