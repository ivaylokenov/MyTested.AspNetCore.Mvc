namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    public class DataStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<ITempDataProvider, CustomTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
