namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class NoEndpointsStartup
    {
        public virtual void ConfigureServices(IServiceCollection services)
            => services.AddMvc(options => options.EnableEndpointRouting = false);

        public virtual void Configure(IApplicationBuilder app) 
            => app.UseMvcWithDefaultRoute();
    }
}
