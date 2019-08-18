namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class CustomStartupWithRouteTesting
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRoutingTesting();
        }

        public void Configure(IApplicationBuilder app)
            => app.UseMvcWithDefaultRoute();
    }
}
