namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using Contracts.Attributes;
    using Internal.TestContexts;

    public class ViewComponentAttributesTestBuilder : BaseAttributesTestBuilder, IViewComponentAttributesTestBuilder
    {
        public ViewComponentAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }
    }
}
