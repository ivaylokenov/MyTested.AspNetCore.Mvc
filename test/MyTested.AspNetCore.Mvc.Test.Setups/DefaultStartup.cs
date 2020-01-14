namespace MyTested.AspNetCore.Mvc.Test.Setups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class DefaultStartup
    {
        public virtual void ConfigureServices(IServiceCollection services) 
            => services.AddMvc();

        public virtual void Configure(IApplicationBuilder app) 
            => app
                .UseRouting()
                .UseEndpoints(endpoints => endpoints
                    .MapDefaultControllerRoute());
    }
}
