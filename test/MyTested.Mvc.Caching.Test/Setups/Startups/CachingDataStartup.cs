namespace MyTested.Mvc.Test.Setups.Startups
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    public class CachingDataStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IMemoryCache, CustomMemoryCache>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
