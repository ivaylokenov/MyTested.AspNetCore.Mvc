namespace MyTested.AspNetCore.Mvc.Test.Setups.ActionFilters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Services;

    public class MyOtherActionFilter : IActionFilter
    {
        private readonly IInjectedService service;

        public MyOtherActionFilter(IInjectedService service)
        {
            this.service = service;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
