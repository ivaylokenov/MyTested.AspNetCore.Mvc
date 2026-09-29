namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("api/deprecated-versioning")]
    public class DeprecatedVersioningController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
