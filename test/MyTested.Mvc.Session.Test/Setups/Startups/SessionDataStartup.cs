namespace MyTested.Mvc.Test.Setups.Startups
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    public class SessionDataStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<ITempDataProvider, CustomTempDataProvider>();
            services.AddSingleton<IMemoryCache, CustomMemoryCache>();
            services.AddTransient<ISessionStore, CustomSessionStore>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
