namespace MyTested.AspNetCore.Mvc.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Setups;

    public class AuthorizationStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
                    options.AddPolicy("Admin",
                    policy => policy.RequireRole("Admin", "Moderator")));

            base.ConfigureServices(services);
        }
    }
}
