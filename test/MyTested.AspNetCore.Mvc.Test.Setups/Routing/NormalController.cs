namespace MyTested.AspNetCore.Mvc.Test.Setups.Routing
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Setups.Filters;

    public class NormalController : Controller
    {
        public static void StaticCall()
        {
        }

        public IActionResult ActionWithoutParameters() => null;

        public IActionResult ActionWithParameters(int id) => null;

        public IActionResult ActionWithStringParameters(string id) => null;

        [HttpPost]
        public IActionResult ActionWithModel(int id, [FromBody]RequestModel model) => null;

        public IActionResult ActionWithMultipleParameters(int id, string text, [FromBody]RequestModel model) => null;

        public IActionResult ActionWithOverloads() => null;

        public IActionResult ActionWithOverloads(int? id) => null;

        [ActionName("AnotherName")]
        public IActionResult ActionWithChangedName() => null;

        public IActionResult QueryString(string first, int second) => null;

        [HttpGet]
        public IActionResult GetMethod() => null;

        // MVC has bug - uncomment when resolved
        //[MyRouteConstraint("id", "5")]
        //public IActionResult ActionWithConstraint(int id)
        //{
        //    return null;
        //}

        public IActionResult FromRouteAction([FromRoute]RequestModel model) => null;

        public IActionResult FromQueryAction([FromQuery]RequestModel model) => null;

        public IActionResult FromFormAction([FromForm]RequestModel model) => null;

        public IActionResult FromHeaderAction([FromHeader]string myHeader) => null;

        public IActionResult FromServicesAction([FromServices]IActionSelector actionSelector) => null;

        [HttpPost]
        public IActionResult UltimateModelBinding(ModelBindingModel model, [FromServices]IUrlHelperFactory urlHelper) => null;

        [Authorize]
        public IActionResult AuthorizedAction() => null;

        [ValidateAntiForgeryToken]
        public IActionResult FiltersAction() => null;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FiltersActionWithModelBinding(int id, [FromBody]RequestModel model) => null;

        [CustomActionFilter]
        public IActionResult CustomFiltersAction() => this.Ok();

        public IActionResult ThrowableAction() => throw new Exception();

        public void VoidAction()
        {
        }
    }
}
