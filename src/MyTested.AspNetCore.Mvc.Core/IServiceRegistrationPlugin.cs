namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IServiceRegistrationPlugin
    {
        Func<ServiceDescriptor, bool> ServiceSelectorPredicate { get; }

        Action<IServiceCollection> ServiceRegistrationDelegate { get; }
    }
}
