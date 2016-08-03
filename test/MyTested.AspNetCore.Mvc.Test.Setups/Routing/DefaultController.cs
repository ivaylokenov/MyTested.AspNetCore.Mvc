namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Files")]
    public class DefaultController : Controller
    {
        public IActionResult Test(string fileName)
        {
            return this.View();
        }

        public IActionResult Download(string fileName)
        {
            return this.View();
        }
    }
}
