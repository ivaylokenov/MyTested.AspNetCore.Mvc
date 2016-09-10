namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class UserComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            return this.Content("Invalid");
        }
    }
}
