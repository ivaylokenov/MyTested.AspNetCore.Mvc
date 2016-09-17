namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class AddTempDataComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            this.ViewContext.TempData.Add("Test", "TempValue");
            this.ViewContext.TempData.Add("Another", "AnotherValue");
            return this.View();
        }
    }
}
