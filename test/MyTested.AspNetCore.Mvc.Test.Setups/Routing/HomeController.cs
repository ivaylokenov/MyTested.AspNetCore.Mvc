namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public string Data { get; set; } = "Test";

        public IActionResult Index() => this.View();

        public async Task<IActionResult> AsyncMethod() 
            => await Task.Run(() => this.Ok(this.Data));

        public IActionResult Contact(int id) => this.Ok(id);

        public IActionResult FailingAction() 
            => throw new InvalidOperationException();

        public void Empty() { }

        public async Task EmptyTask() => await Task.CompletedTask;

        public async Task<IActionResult> CancelledTask(int id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return await Task.FromResult(Ok($"Cancelled with id: {id}"));
            }

            return await Task.FromResult(Ok(id));
        }
    }
}
