namespace MyTested.AspNetCore.Mvc.Test.Setups.Routes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;

    public class NormalController : Controller
    {
        public static void StaticCall()
        {
        }

        public IActionResult ActionWithoutParameters()
        {
            return null;
        }

        public IActionResult ActionWithParameters(int id)
        {
            return null;
        }

        public IActionResult ActionWithStringParameters(string id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult ActionWithModel(int id, [FromBody]RequestModel model)
        {
            return null;
        }

        public IActionResult ActionWithMultipleParameters(int id, string text, [FromBody]RequestModel model)
        {
            return null;
        }

        public IActionResult ActionWithOverloads()
        {
            return null;
        }

        public IActionResult ActionWithOverloads(int? id)
        {
            return null;
        }

        [ActionName("AnotherName")]
        public IActionResult ActionWithChangedName()
        {
            return null;
        }

        public IActionResult QueryString(string first, int second)
        {
            return null;
        }

        [HttpGet]
        public IActionResult GetMethod()
        {
            return null;
        }

        // MVC has bug - uncomment when resolved
        //[MyRouteConstraint("id", "5")]
        //public IActionResult ActionWithConstraint(int id)
        //{
        //    return null;
        //}

        public IActionResult FromRouteAction([FromRoute]RequestModel model)
        {
            return null;
        }

        public IActionResult FromQueryAction([FromQuery]RequestModel model)
        {
            return null;
        }

        public IActionResult FromFormAction([FromForm]RequestModel model)
        {
            return null;
        }

        public IActionResult FromHeaderAction([FromHeader]string myHeader)
        {
            return null;
        }

        public IActionResult FromServicesAction([FromServices]IActionSelector actionSelector)
        {
            return null;
        }

        [HttpPost]
        public IActionResult UltimateModelBinding(ModelBindingModel model, [FromServices]IUrlHelperFactory urlHelper)
        {
            return null;
        }

        public void VoidAction()
        {
        }
    }
}
