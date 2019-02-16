namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartupWithConfigureContainer
    {
        public void ConfigureServices(IServiceCollection services) 
            => services.AddMvc();

        public void ConfigureContainer(CustomContainer services)
            => services.Services.AddTransient<IInjectedService, InjectedService>();

        public void Configure(IApplicationBuilder app) 
            => app.UseMvcWithDefaultRoute();
    }
}
