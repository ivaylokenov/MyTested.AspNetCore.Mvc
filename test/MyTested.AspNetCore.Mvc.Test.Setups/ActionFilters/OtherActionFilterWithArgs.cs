namespace MyTested.AspNetCore.Mvc.Test.Setups.ActionFilters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Services;

    public class OtherActionFilterWithArgs : IActionFilter
    {
        private readonly IInjectedService service;

        public OtherActionFilterWithArgs(IInjectedService service, int maxRequestPerSecond)
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
