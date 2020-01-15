namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/versioning")]
    public class QueryVersioningController : Controller
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
