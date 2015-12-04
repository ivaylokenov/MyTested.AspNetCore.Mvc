namespace MyTested.Mvc.Tests.Setups
{
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System;

    public class CustomStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplacableInjectedService>();
        }

        public IServiceProvider ConfigureServicesAndBuildProvider(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplacableInjectedService>();
            return services.BuildServiceProvider();
        }
    }
}
