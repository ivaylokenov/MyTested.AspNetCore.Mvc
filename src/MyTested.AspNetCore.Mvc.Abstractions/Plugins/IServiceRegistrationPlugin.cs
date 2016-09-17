namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IServiceRegistrationPlugin
    {
        Func<ServiceDescriptor, bool> ServiceSelectorPredicate { get; }

        Action<IServiceCollection> ServiceRegistrationDelegate { get; }
    }
}
