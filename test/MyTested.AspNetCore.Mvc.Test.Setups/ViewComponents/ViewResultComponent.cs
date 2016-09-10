namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class ViewResultComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string viewType)
        {
            if (viewType == null)
            {
                return View();
            }

            if (viewType == "custom")
            {
                return View("Custom");
            }

            if (viewType == "model")
            {
                return View(new ResponseModel { StringValue = "TestValue" });
            }

            return View("SomeView", new ResponseModel { IntegerValue = 10 });
        }
    }
}
