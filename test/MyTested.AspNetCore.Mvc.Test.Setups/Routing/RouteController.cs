namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
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

        [HttpGet("[action]/{id}")]
        public IActionResult Action(int id)
        {
            return this.View();
        }
    }
}
