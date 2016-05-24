namespace MyTested.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class ServicesController : Controller
    {
        private readonly IScopedService scopedService;

        public ServicesController(IScopedService scopedService)
        {
            this.scopedService = scopedService;
            if (this.scopedService != null)
            {
                this.scopedService.Value = "Constructor";
            }
        }
        
        public string SetValue()
        {
            var service = this.HttpContext.RequestServices.GetService<IScopedService>();
            service.Value = "Scoped";
            service = this.HttpContext.RequestServices.GetService<IScopedService>();
            return service.Value;
        }

        public string DoNotSetValue()
        {
            var service = this.HttpContext.RequestServices.GetService<IScopedService>();
            return service.Value;
        }

        public string FromServices([FromServices]IScopedService service)
        {
            return service.Value;
        }
    }
}
