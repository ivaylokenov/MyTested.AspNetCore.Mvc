namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using System;
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartupWithCustomServiceProvider
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IInjectedService, InjectedService>();

            services.AddCoreTesting();

            var customServiceProvider = new CustomServiceProvider();

            foreach (var service in services)
            {
                customServiceProvider.Add(service);
            }

            customServiceProvider.Build();

            return customServiceProvider;
        }

        public void Configure(IApplicationBuilder app)
            => app.UseMvcWithDefaultRoute();
    }
}
