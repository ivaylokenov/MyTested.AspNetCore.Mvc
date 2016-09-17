namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class UrlComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => this.Content(this.Url.Action("UrlAction", "Mvc"));
    }
}
