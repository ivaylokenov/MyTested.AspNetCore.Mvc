namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/header-versioning")]
    public class HeaderVersioningController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
