namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AddSessionComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            this.HttpContext.Session.SetInt32("Integer", 1);
            this.HttpContext.Session.SetString("String", "Text");
            this.HttpContext.Session.Set("Bytes", new byte[] { 1, 2, 3 });
            return this.View();
        }
    }
}
