namespace MyTested.Mvc.Test.Setups.Controllers
{
    using System;
    using Internal.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class PocoController
    {
        private readonly IServiceProvider services;

        public PocoController()
        {
            this.services = TestApplication.Services;
        }

        public HttpContext CustomHttpContext => this.services.GetRequiredService<IHttpContextAccessor>().HttpContext;
        
        public IActionResult SessionAction()
        {
            if (this.CustomHttpContext.Session.GetString("test") != null)
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
