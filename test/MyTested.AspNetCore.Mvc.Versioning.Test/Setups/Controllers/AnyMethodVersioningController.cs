namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/any-method-versioning")]
    public class AnyMethodVersioningController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult Get() => this.Ok();

        [Route("")]
        public IActionResult Any() => this.Ok();
    }
}
