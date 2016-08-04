namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
