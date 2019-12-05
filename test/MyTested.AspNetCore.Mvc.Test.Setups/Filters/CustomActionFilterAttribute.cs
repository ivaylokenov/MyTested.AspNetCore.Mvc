namespace MyTested.AspNetCore.Mvc.Test.Setups.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // change controller property to save state

            var query = context.HttpContext.Request.Query;

            if (query.ContainsKey("throw"))
            {
                throw new Exception();
            }

            if (query.ContainsKey("result"))
            {
                context.Result = new BadRequestResult();
            }
            
            base.OnActionExecuting(context);
        }
    }
}
