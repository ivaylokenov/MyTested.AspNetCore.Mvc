namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/neutral-mixed-method")]
    public class NeutralMixedMethodController : ControllerBase
    {
        [HttpGet]
        [ApiVersionNeutral]
        public IActionResult Get() => this.Ok();

        [HttpPut]
        public IActionResult Put() => this.Ok();
    }
}
