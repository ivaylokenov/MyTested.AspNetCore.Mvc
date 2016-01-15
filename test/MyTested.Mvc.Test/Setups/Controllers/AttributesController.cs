namespace MyTested.Mvc.Tests.Setups.Controllers
{
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;

    [AllowAnonymous]
    [Route("api/test", Name = "TestRouteAttributes", Order = 1)]
    public class AttributesController : Controller
    {
        [AllowAnonymous]
        public IActionResult WithAttributesAndParameters(int id)
        {
            return this.Ok(id);
        }
    }
}
