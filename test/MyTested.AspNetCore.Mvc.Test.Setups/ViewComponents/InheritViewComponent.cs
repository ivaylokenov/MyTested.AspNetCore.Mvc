namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ResponseCache]
    public class InheritViewComponent : BaseInheritViewComponent
    {
    }

    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public abstract class BaseInheritViewComponent : ViewComponent
    {
        public virtual IViewComponentResult Invoke() => this.View();
    }
}
