namespace MyTested.AspNetCore.Mvc.Test.Setups.ActionFilters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Services;

    public class CustomActionFilterWithArgs : IActionFilter
    {
        private readonly IInjectedService service;

        public CustomActionFilterWithArgs(IInjectedService service, int maxRequestPerSecond)
        {
            this.service = service;
            this.MaxRequestPerSecond = maxRequestPerSecond;
        }

        public int MaxRequestPerSecond { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
