namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/unversioned-and-implicit")]
    public class ImplicitSameRouteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => this.Ok();
    }
}
