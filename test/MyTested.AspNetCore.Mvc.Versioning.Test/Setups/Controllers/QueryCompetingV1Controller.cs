namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/query-competing")]
    public class QueryCompetingV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
