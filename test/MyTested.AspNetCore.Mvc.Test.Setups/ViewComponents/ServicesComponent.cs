namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class ServicesComponent : ViewComponent
    {
        public ServicesComponent(IInjectedService service)
        {
            this.Service = service;
        }

        public IInjectedService Service { get; private set; }

        public IViewComponentResult Invoke() => this.View();
    }
}
