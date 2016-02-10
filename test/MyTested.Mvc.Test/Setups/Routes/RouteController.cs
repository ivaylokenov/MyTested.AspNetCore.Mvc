namespace MyTested.Mvc.Tests.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;

    [Route("AttributeController")]
    public class RouteController : Controller
    {
        [Route("AttributeAction")]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
