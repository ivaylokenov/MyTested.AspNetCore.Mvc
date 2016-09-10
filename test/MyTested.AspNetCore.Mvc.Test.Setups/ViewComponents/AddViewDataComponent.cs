namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class AddViewDataComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            this.ViewData["Test"] = "DataValue";
            this.ViewData["Another"] = "AnotherValue";
            return this.View();
        }
    }
}
