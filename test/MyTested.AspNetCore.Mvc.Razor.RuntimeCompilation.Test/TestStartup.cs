namespace MyTested.AspNetCore.Mvc.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Setups;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services
                .AddRazorPages();
        }
    }
}
