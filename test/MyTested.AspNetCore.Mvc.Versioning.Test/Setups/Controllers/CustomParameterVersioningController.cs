namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{ver:apiVersion}/custom-parameter")]
    public class CustomParameterVersioningController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
