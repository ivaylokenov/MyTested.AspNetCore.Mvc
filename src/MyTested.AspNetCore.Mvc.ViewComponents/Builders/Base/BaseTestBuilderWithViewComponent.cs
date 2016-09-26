namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Internal.TestContexts;
    using Utilities.Validators;
    
    /// <summary>
    /// Base class for all test builders with view component.
    /// </summary>
    public abstract class BaseTestBuilderWithViewComponent : BaseTestBuilderWithActionContext, IBaseTestBuilderWithViewComponent
    {
        private ViewComponentTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithViewComponent"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ViewComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public BaseTestBuilderWithViewComponent(ViewComponentTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets the view component which will be tested.
        /// </summary>
        /// <value>View component which will be tested.</value>
        public object ViewComponent => this.TestContext.Component;

        /// <summary>
        /// Gets the view component attributes which will be tested.
        /// </summary>
        /// <value>View component attributes which will be tested.</value>
        public IEnumerable<object> ViewComponentAttributes => this.TestContext.ComponentAttributes;

        /// <summary>
        /// Gets the currently used <see cref="ViewComponentTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ViewComponentTestContext"/>.</value>
        public new ViewComponentTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value.Component, nameof(this.ViewComponent));
                this.testContext = value;
            }
        }
    }
}
