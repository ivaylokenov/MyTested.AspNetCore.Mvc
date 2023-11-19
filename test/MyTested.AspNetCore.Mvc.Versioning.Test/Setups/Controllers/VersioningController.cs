namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VersioningController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();

        [HttpGet]
        [MapToApiVersion("3.0")]
        public IActionResult SpecificVersion() => this.Ok();
    }
}
