namespace WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
                $"{this.dateTime.GetTime().ToShortDateString()} {this.data.GetData()}");

        public IActionResult RedirectToIndex()
            => this.RedirectToAction(nameof(this.Index));

        public IActionResult Privacy() => this.View();
    }
}
