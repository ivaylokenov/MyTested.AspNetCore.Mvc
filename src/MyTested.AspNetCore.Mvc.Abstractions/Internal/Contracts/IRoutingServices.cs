namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IRoutingServices
    {
        IServiceCollection ServiceCollection { get; set; }

        IServiceProvider ServiceProvider { get; set; }
    }
}
