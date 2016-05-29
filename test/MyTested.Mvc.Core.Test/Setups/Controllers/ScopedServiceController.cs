namespace MyTested.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class ScopedServiceController : Controller
    {
        private readonly IScopedService scopedService;

        public ScopedServiceController(IScopedService scopedService)
        {
            this.scopedService = scopedService;
        }

        public IActionResult Index()
        {
            return this.Ok(this.scopedService.Value);
        }
    }
}
