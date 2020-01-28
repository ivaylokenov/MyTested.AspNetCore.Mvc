namespace MyTested.AspNetCore.Mvc.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Services;
    using Setups.ActionFilters;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IInjectedService, InjectedService>();
            services.AddScoped<MyActionFilter>();
            services.AddScoped<MyActionFilterWithArgs>();

            services.AddAuthorization(options =>
                options.AddPolicy("Admin",
                policy => policy.RequireRole("Admin", "Moderator")));

            base.ConfigureServices(services);
        }
    }
}
