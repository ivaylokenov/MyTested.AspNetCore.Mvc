namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    // Used by the route testing services to fake the actual action call.
    public class RouteTestingActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue;

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.Result = new RouteActionResultMock();

            return Task.CompletedTask;
        }
    }
}
