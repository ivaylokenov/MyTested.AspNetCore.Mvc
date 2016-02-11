namespace MyTested.Mvc.Tests.Setups.Startups
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class RoutesStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "files",
                    areaName: "Files",
                    template: "Files/{controller=Default}/{action=Test}/{fileName=None}");

                routes.MapRoute(
                    name: "test",
                    template: "Test/{action=Index/{id?}",
                    defaults: new { controller = "Test" },
                    constraints: new { controller = "Test" },
                    dataTokens: new { random = "value" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
