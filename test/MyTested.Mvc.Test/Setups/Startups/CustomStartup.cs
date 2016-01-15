namespace MyTested.Mvc.Tests.Setups.Startups
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
        }

        public IServiceProvider ConfigureServicesAndBuildProvider(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
            return services.BuildServiceProvider();
        }
    }
}
