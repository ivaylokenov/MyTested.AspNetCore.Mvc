namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class AddViewBagComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            this.ViewBag.Test = "BagValue";
            this.ViewBag.Another = "AnotherValue";
            return this.View();
        }
    }
}
