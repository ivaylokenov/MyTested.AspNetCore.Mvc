namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IRoutingServiceRegistrationPlugin
    {
        Action<IServiceCollection> RoutingServiceRegistrationDelegate { get; }
    }
}
