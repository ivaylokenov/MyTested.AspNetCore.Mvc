namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using Microsoft.AspNetCore.Mvc;

    public class TestController : Controller
    {
        public IActionResult Index() => this.View();

        public IActionResult WithDataToken(string random) => this.View();
    }
}
