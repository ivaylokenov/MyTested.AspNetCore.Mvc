﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
        }

        public void Configure(IApplicationBuilder app) => app
            .UseRouting()
            .UseEndpoints(endpoints => endpoints
                .MapDefaultControllerRoute());

        public IServiceProvider ConfigureServicesAndBuildProvider(IServiceCollection services)
        {
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
            return services.BuildServiceProvider();
        }
    }
}
