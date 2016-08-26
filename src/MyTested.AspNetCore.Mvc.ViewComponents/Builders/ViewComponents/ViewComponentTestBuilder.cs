namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using Base;
    using Contracts.ViewComponents;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing view components.
    /// </summary>
    public class ViewComponentTestBuilder : BaseTestBuilderWithViewComponent, IViewComponentTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ViewComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewComponentTestBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
