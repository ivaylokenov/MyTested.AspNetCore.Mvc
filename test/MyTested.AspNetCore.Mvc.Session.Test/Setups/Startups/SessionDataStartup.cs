namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;

    public class SessionDataStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<ISessionStore, CustomSessionStore>();
        }

        public void Configure(IApplicationBuilder app) => app
            .UseRouting()
            .UseEndpoints(endpoints => endpoints
                .MapDefaultControllerRoute());
    }
}
