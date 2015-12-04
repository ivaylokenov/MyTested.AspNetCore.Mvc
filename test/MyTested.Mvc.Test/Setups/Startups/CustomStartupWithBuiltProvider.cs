namespace MyTested.Mvc.Tests.Setups.Startups
{
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System;

    public class CustomStartupWithBuiltProvider
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
            return services.BuildServiceProvider();
        }
    }
}
