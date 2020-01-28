namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Routing;

    public class RouteHandlerMock
    {
        internal static readonly IRouter Null = new RouteHandler(c => Task.CompletedTask);
    }
}
