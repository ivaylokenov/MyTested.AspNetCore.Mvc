namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AccessorComponent : ViewComponent
    {
#pragma warning disable ASPDEPR006 // Obsolete IActionContextAccessor, kept to verify the library still supports applications which use it.
        public AccessorComponent(IActionContextAccessor accessor)
        {
            this.ActionContext = accessor.ActionContext as ViewContext;
        }
#pragma warning restore ASPDEPR006 // Obsolete IActionContextAccessor, kept to verify the library still supports applications which use it.

        public ViewContext ActionContext { get; private set; }

        public IViewComponentResult Invoke() => this.View();
    }
}
