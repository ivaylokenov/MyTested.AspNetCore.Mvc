namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.ViewComponents;
    using Builders.ViewComponents;
    using Internal.Application;
    using Internal.TestContexts;

    public class MyViewComponent<TViewComponent> : ViewComponentBuilder<TViewComponent>
        where TViewComponent : class
    {
        static MyViewComponent()
        {
            TestApplication.TryInitialize();
        }

        public MyViewComponent()
            : this((TViewComponent)null)
        {
        }

        public MyViewComponent(TViewComponent controller)
            : this(() => controller)
        {
        }

        public MyViewComponent(Func<TViewComponent> construction)
            : base(new ViewComponentTestContext { ViewComponentConstruction = construction })
        {
        }

        public static IViewComponentBuilder<TViewComponent> Instance()
        {
            return Instance((TViewComponent)null);
        }

        public static IViewComponentBuilder<TViewComponent> Instance(TViewComponent viewComponent)
        {
            return Instance(() => viewComponent);
        }

        public static IViewComponentBuilder<TViewComponent> Instance(Func<TViewComponent> construction)
        {
            return new MyViewComponent<TViewComponent>(construction);
        }
    }
}
