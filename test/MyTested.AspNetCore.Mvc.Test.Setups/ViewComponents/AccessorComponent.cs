namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AccessorComponent : ViewComponent
    {
        public AccessorComponent(IActionContextAccessor accessor)
        {
            this.ActionContext = accessor.ActionContext as ViewContext;
        }

        public ViewContext ActionContext { get; private set; }

        public IViewComponentResult Invoke() => this.View();
    }
}
