namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System;
    using Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class RoutingServices : IRoutingServices
    {
        public IServiceCollection ServiceCollection { get; set; }

        public IServiceProvider ServiceProvider { get; set; }
    }
}
