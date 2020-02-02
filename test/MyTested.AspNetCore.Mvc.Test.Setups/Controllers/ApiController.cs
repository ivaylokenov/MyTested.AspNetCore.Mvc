namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ActionFilters;
    using Models;
    using Pipeline;

    [ApiController]
    [Consumes("application/json", "application/xml")]
    [Produces("application/json", "application/xml", Type = typeof(ResponseModel), Order = 1)]
    [MiddlewareFilter(typeof(Pipeline), Order = 2)]
    [ServiceFilter(typeof(CustomActionFilter), Order = 2)]
    [TypeFilter(typeof(CustomActionFilterWithArgs), Order = 2, Arguments = new object[] { 10 })]
    public class ApiController : Controller
    {
        [Route("/route")]
        public IActionResult Get() => this.Ok();

        [FormatFilter]
        [Route("/post-route")]
        [Consumes("application/pdf", "application/javascript")]
        [Produces("application/pdf", "application/javascript", Type = typeof(RequestModel), Order = 2)]
        public IActionResult Post() => this.Ok();
    }
}
