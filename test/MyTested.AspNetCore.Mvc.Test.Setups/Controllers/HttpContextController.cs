namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HttpContextController : Controller
    {
        public HttpContextController(IHttpContextAccessor httpContextAccessor)
        {
            this.Context = httpContextAccessor.HttpContext;
        }

        public HttpContext Context { get; private set; }
    }
}
