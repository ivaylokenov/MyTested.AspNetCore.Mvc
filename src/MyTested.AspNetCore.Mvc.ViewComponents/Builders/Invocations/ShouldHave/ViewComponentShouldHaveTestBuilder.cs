namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldHave
{
    using Base;
    using Contracts.Invocations;
    using Internal.TestContexts;
    using Utilities.Validators;

    public partial class ViewComponentShouldHaveTestBuilder<TInvocationResult>
        : BaseTestBuilderWithComponentBuilder<IAndViewComponentResultTestBuilder<TInvocationResult>>, IViewComponentShouldHaveTestBuilder<TInvocationResult>
    {
        private ViewComponentTestContext testContext;

        public ViewComponentShouldHaveTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;
        }

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
                CommonValidator.CheckForNullReference(value.Component, nameof(this.TestContext.Component));
                this.testContext = value;
            }
        }

        protected override IAndViewComponentResultTestBuilder<TInvocationResult> SetBuilder()
            => new AndViewComponentResultTestBuilder<TInvocationResult>(this.TestContext);
    }
}
