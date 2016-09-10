namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    public class HttpRequestComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (this.Request.Form.Any(f => f.Key == "Test"))
            {
                return this.View();
            }

            return this.Content("Invalid");
        }
    }
}
