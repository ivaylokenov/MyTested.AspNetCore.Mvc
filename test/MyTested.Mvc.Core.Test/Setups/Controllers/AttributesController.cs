namespace MyTested.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
