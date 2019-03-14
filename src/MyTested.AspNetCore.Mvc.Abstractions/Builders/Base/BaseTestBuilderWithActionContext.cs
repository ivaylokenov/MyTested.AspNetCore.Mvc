namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Internal.TestContexts;
    using Utilities.Validators;

    public abstract class BaseTestBuilderWithActionContext : BaseTestBuilderWithComponent
    {
        private ActionTestContext testContext;

        protected BaseTestBuilderWithActionContext(ActionTestContext testContext) 
            : base(testContext) 
            => this.TestContext = testContext;

        /// <summary>
        /// Gets the currently used <see cref="ActionTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ActionTestContext"/>.</value>
        public new ActionTestContext TestContext
        {
            get => this.testContext;

            private set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }
    }
}
