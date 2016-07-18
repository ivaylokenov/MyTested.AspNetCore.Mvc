namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Internal.TestContexts;
    using Utilities.Validators;

    public class CoreAttributesTestBuilder : BaseAttributesTestBuilder
    {
        private ControllerTestContext testContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public CoreAttributesTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

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
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                CommonValidator.CheckForNullReference(value.Controller, nameof(TestContext.Controller));
                this.testContext = value;
            }
        }

        public object Controller => this.TestContext.Controller;
    }
}
