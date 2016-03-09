namespace MyTested.Mvc.Test.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Contact(int id)
        {
            return this.View();
        }
    }
}
