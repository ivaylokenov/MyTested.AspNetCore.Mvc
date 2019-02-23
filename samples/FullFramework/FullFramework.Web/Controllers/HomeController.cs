namespace FullFramework.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class HomeController : Controller
    {
        private readonly IDataService data;

        public HomeController(IDataService data) 
            => this.data = data;

        public IActionResult Index()
            => this.View(
                nameof(this.Index),
                $"{this.data.GetData()}");

        public IActionResult RedirectToIndex()
            => this.RedirectToAction(nameof(this.Index));

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
