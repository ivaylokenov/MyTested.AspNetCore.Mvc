namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index() => this.View();

        public async Task<IActionResult> AsyncMethod() 
            => await Task.Run(() => this.Ok("Test"));

        public IActionResult Contact(int id) => this.Ok(id);

        public IActionResult FailingAction() 
            => throw new InvalidOperationException();
    }
}
