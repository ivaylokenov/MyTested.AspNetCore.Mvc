namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class FullConfigureStartup
    {
        public virtual void ConfigureTestServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public virtual void ConfigureTest(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
