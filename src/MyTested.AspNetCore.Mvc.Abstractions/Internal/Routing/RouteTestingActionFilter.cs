namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;
    using TestContexts;

    // Used by the route testing services to short-circuit the actual action call and retrieve context information.
    public class RouteTestingActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue;

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Features.Set(new ExecutionTestContext
            {
                Controller = context.Controller,
                ActionArguments = context.ActionArguments
            });

            context.Result = new RouteActionResultMock();

            return Task.CompletedTask;
        }
    }
}
