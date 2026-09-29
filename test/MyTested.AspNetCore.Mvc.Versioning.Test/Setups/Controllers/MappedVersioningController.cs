namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/mapped-versioning")]
    public class MappedVersioningController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult SpecificVersion() => this.Ok();
    }
}
