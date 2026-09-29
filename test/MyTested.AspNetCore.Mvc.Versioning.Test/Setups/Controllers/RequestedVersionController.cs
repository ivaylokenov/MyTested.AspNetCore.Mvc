namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/requested-version")]
    public class RequestedVersionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok(this.HttpContext.RequestedApiVersion);
    }
}
