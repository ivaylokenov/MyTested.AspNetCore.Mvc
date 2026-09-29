namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class ActionContextController : Controller
    {
#pragma warning disable ASPDEPR006 // Obsolete IActionContextAccessor, kept to verify the library still supports applications which use it.
        public ActionContextController(IActionContextAccessor actionContextAccessor)
        {
            this.Context = actionContextAccessor.ActionContext;
        }
#pragma warning restore ASPDEPR006 // Obsolete IActionContextAccessor, kept to verify the library still supports applications which use it.

        public ActionContext Context { get; private set; }

        public IActionResult Index()
        {
            return this.Ok(this.Context);
        }
    }
}
