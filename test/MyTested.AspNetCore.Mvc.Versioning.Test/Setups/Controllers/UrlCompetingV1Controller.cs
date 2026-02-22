namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/competing")]
    public class UrlCompetingV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
