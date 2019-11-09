namespace MyTested.AspNetCore.Mvc.Test.Setups
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "")]
    public class AuthorizationController : Controller
    {
        public IActionResult Index() => this.View();

        [Authorize(AuthenticationSchemes = "Cookies", Policy = "Admin")]
        [HttpGet]
        public IActionResult NormalActionWithAuthorizeAttribute()
        {
            return this.Ok();
        }
    }
}
