namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class EndpointsRoutingStartup
    {
        public virtual void ConfigureServices(IServiceCollection services)
            => services.AddMvc();

        public virtual void Configure(IApplicationBuilder app) => app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "files",
                    areaName: "Files",
                    pattern: "Files/{controller=Default}/{action=Test}/{fileName=None}");

                endpoints.MapControllerRoute(
                    name: "test",
                    pattern: "Test/{action=Index}/{id?}",
                    defaults: new { controller = "Test" },
                    constraints: new { id = @"\d" },
                    dataTokens: new { random = "value", another = "token" });
            });
    }
}
