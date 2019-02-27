namespace WebStartup.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class HomeController : Controller
    {
        public IActionResult Index() => this.View();

        public IActionResult Privacy() => this.View();

        public IActionResult RedirectToIndex()
            => this.RedirectToAction(nameof(this.Index));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
