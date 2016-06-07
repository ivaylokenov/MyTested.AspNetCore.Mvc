namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class ActionContextController : Controller
    {
        public ActionContextController(IActionContextAccessor actionContextAccessor)
        {
            this.Context = actionContextAccessor.ActionContext;
        }

        public ActionContext Context { get; private set; }

        public IActionResult Index()
        {
            return this.Ok(this.Context);
        }
    }
}
