namespace MyTested.Mvc.Tests.Setups.Startups
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartupWithBuiltProvider
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
            return services.BuildServiceProvider();
        }
    }
}
