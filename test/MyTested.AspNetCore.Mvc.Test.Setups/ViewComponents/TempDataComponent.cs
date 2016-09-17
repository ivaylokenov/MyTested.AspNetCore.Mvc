namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class TempDataComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var tempDataValue = this.ViewContext.TempData["Test"];
            if (tempDataValue != null)
            {
                return this.Content(tempDataValue as string);
            }

            return this.View();
        }
    }
}
