namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IDefaultRegistrationPlugin
    {
        long Priority { get; }
        
        Action<IServiceCollection> DefaultServiceRegistrationDelegate { get; }
    }
}
