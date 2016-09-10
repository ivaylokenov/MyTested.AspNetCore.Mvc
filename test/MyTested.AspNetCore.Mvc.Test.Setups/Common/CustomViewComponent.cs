namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.AspNetCore.Mvc;

    public class CustomViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
