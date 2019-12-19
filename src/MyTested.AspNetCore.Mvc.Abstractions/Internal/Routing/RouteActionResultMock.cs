﻿namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class RouteActionResultMock : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
            => Task.CompletedTask;
    }
}
