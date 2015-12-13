namespace MyTested.Mvc.Tests.Setups.Controllers
{
    using Microsoft.AspNet.Mvc;

    public class NoAttributesController : Controller
    {
        public IActionResult WithParameter(int id)
        {
            return this.Ok(id);
        }

        public void VoidAction()
        {
        }
    }
}
