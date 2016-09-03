namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using System.Reflection;
    using Components;
    using Contracts.ViewComponents;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Used for building the view component which will be tested.
    /// </summary>
    /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
    public partial class ViewComponentBuilder<TViewComponent> : BaseComponentBuilder<TViewComponent, ViewComponentTestContext, IAndViewComponentBuilder<TViewComponent>>, IAndViewComponentBuilder<TViewComponent>
        where TViewComponent : class
    {
        public ViewComponentBuilder(ViewComponentTestContext testContext)
            : base(testContext)
        {
        }

        protected override string ComponentName => "view component";

        protected override bool IsValidComponent =>
            ViewComponentConventions.IsComponent(typeof(TViewComponent).GetTypeInfo());

        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> AndAlso() => this;

        /// <inheritdoc />
        public IViewComponentTestBuilder ShouldHave()
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();
            return new ViewComponentTestBuilder(this.TestContext);
        }

        protected override IAndViewComponentBuilder<TViewComponent> SetBuilder() => this;
    }
}
