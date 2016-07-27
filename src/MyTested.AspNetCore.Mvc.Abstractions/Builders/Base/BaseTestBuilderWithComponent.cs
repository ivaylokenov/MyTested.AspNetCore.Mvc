namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders with component.
    /// </summary>
    public abstract class BaseTestBuilderWithComponent : BaseTestBuilder, IBaseTestBuilderWithComponent
    {
        private ComponentTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithComponent"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithComponent(ComponentTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }
        
        /// <summary>
        /// Gets the currently used <see cref="ComponentTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ComponentTestContext"/>.</value>
        public new ComponentTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }
    }
}
