namespace MyTested.AspNetCore.Mvc.Test.Setups
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "Admin", Roles = "Admin")]
    public class AuthorizationController : Controller
    {
        public IActionResult Index() => this.View();

        [Authorize(AuthenticationSchemes = "Cookies", Policy = "Admin", Roles = "Admin")]
        [HttpGet]
        public IActionResult NormalActionWithAuthorizeAttribute()
        {
            return this.Ok();
        }
    }
}
