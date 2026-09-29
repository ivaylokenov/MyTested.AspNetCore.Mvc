namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersionNeutral]
    [Route("api/neutral-query")]
    public class NeutralQueryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
