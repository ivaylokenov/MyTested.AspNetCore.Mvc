namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/query-competing")]
    public class QueryCompetingV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => this.Ok();
    }
}
