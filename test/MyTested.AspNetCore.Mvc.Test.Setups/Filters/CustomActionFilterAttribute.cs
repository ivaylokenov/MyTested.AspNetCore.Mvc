namespace MyTested.AspNetCore.Mvc.Test.Setups.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Utilities.Extensions;

    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var query = context.HttpContext.Request.Query;

            if (query.ContainsKey("controller"))
            {
                context.Controller.Exposed().Data = "ActionFilter";
            }

            if (query.ContainsKey("throw"))
            {
                throw new Exception();
            }

            if (query.ContainsKey("result"))
            {
                context.Result = new BadRequestResult();
            }

            context.RouteData.Values.Add("ActionFilter", "Route Value");
            context.ActionDescriptor.Properties.Add("ActionFilter", "Descriptor Value");
            context.ModelState.AddModelError("ActionFilter", "Model State Value");
            context.HttpContext.Features.Set(context.Controller);

            base.OnActionExecuting(context);
        }
    }
}
