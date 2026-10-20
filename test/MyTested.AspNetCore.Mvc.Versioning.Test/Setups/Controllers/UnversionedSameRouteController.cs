namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/unversioned-and-implicit")]
    public class UnversionedSameRouteController : Controller
    {
        [HttpGet]
        public IActionResult Get() => this.Ok();
    }
}
