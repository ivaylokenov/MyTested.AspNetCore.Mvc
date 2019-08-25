namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
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
            services.Replace<IMemoryCache, CustomMemoryCache>(ServiceLifetime.Singleton);
        }

        public void Configure(IApplicationBuilder app) => app
            .UseRouting()
            .UseEndpoints(endpoints => endpoints
                .MapDefaultControllerRoute());
    }
}
