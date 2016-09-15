namespace NoStartup.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class HomeController : ControllerBase
    {
        private readonly IService service;

        public HomeController(IService myService)
        {
            this.service = myService;
        }

        public async Task<IActionResult> Index()
            => await Task.FromResult(this.Ok(this.service.GetData()));
    }
}
