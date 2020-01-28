namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomConfigureFilter : IStartupConfigureServicesFilter, IStartupConfigureContainerFilter<CustomContainer>
    {
        public Action<IServiceCollection> ConfigureServices(Action<IServiceCollection> next)
            => services =>
            {
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                next(services);
            };

        public Action<CustomContainer> ConfigureContainer(Action<CustomContainer> next)
            => container =>
            {
                container.Services.AddScoped<IScopedService, ScopedService>();
                next(container);
            };
    }
}
