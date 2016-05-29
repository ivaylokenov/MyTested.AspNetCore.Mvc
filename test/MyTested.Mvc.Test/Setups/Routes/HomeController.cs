namespace MyTested.Mvc.Test.Setups.Routes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> AsyncMethod()
        {
            return await Task.Run(() => this.Ok());
        }

        public IActionResult Contact(int id)
        {
            return this.View();
        }
    }
}
