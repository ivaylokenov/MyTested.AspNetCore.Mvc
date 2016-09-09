namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class SessionComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (this.HttpContext.Session.GetString("test") != null)
            {
                return this.View();
            }

            return this.Content("Invalid");
        }
    }
}
