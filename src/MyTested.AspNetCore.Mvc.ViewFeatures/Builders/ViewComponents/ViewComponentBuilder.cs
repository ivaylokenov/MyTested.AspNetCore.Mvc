namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using Base;
    using Contracts.ViewComponents;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    public class ViewComponentBuilder<TViewComponent> : BaseTestBuilder, IAndViewComponentBuilder<TViewComponent>
        where TViewComponent : class
    {
        public ViewComponentBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }

        public IAndViewComponentBuilder<TViewComponent> AndAlso() => this;
    }
}
