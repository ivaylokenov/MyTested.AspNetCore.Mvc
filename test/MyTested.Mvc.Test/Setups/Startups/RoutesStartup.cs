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
                    name: "custom",
                    template: "CustomRoute",
                    defaults: new { controller = "Normal", action = "FromRouteAction", integer = 1, @string = "test" });

                routes.MapRoute(
                    name: "test",
                    template: "Test/{action=Index}/{id?}",
                    defaults: new { controller = "Test" },
                    constraints: new { controller = "Test" },
                    dataTokens: new { random = "value" });

                routes.MapRoute(
                    name: "Redirect",
                    template: "api/Redirect/{action}",
                    defaults: new { controller = "NoAttributes" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
