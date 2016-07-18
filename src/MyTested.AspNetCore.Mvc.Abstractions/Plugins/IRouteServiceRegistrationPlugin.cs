namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IRouteServiceRegistrationPlugin
    {
        Action<IServiceCollection> RouteServiceRegistrationDelegate { get; }
    }
}
