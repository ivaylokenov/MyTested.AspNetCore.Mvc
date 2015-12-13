namespace MyTested.Mvc.Tests.Setups.Controllers
{
    using Microsoft.AspNet.Mvc;
    using Services;

    public class NoParameterlessConstructorController : Controller
    {
        public NoParameterlessConstructorController(IInjectedService service, IAnotherInjectedService anotherService)
        {
            this.Service = service;
            this.AnotherInjectedService = anotherService;
        }

        public IInjectedService Service { get; private set; }

        public IAnotherInjectedService AnotherInjectedService { get; private set; }

        public IActionResult OkAction()
        {
            return this.Ok();
        }
    }
}
