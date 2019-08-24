namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class FullConfigureStartup
    {
        public virtual void ConfigureTestServices(IServiceCollection services) 
            => services.AddMvc();

        public virtual void ConfigureTest(
            IApplicationBuilder app, 
            IHostEnvironment env, 
            ILoggerFactory loggerFactory,
            IHostApplicationLifetime appLifetime) 
            => app.UseMvcWithDefaultRoute();
    }
}
