﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartupWithBuiltProvider
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();

            services.AddCoreTesting();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app) => app
            .UseRouting()
            .UseEndpoints(endpoints => endpoints
                .MapDefaultControllerRoute());
    }
}
