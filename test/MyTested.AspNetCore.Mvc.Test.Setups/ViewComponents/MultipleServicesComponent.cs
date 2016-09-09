namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class MultipleServicesComponent : ViewComponent
    {
        public MultipleServicesComponent(IInjectedService injectedService, IAnotherInjectedService anotherService)
        {
            this.InjectedService = injectedService;
            this.AnotherService = anotherService;
        }

        public IInjectedService InjectedService { get; private set; }

        public IAnotherInjectedService AnotherService { get; private set; }

        public IViewComponentResult Invoke() => this.View();
    }
}
