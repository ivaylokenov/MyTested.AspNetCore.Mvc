namespace MyTested.AspNetCore.Mvc.Test.Setups.Pipeline
{
    using ActionFilters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Services;

    public class PipelineController : Controller
    {
        public string Data { get; set; }

        public IInjectedService Service { get; private set; }

        public PipelineController(IInjectedService service)
            => this.Service = service;

        public IActionResult Action() => this.Ok();

        [CustomActionFilter]
        public IActionResult FilterAction() => this.Ok();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.Data = "ControllerFilter";

            context.RouteData.Values.Add(this.Data, "Route Value");
            context.ActionDescriptor.Properties.Add(this.Data, "Descriptor Value");
            context.ModelState.AddModelError(this.Data, "Model State Value");
            context.HttpContext.Features.Set(this);
        }
    }
}
