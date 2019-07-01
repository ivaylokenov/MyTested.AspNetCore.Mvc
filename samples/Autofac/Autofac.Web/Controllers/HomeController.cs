namespace Autofac.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class HomeController : Controller
    {
        private readonly IDataService data;
        private readonly IDateTimeService dateTime;

        public HomeController(IDataService data, IDateTimeService dateTime)
        {
            this.data = data;
            this.dateTime = dateTime;
        }

        public IActionResult Index() 
            => this.View(
                nameof(this.Index), 
                $"{this.dateTime.GetTime().ToString("M/d/yyyy")} {this.data.GetData()}");

        public IActionResult RedirectToIndex()
            => this.RedirectToAction(nameof(this.Index));

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
