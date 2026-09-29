namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;

    public class AssumeDefaultVersionStartup : TestStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.Configure<ApiVersioningOptions>(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
        }
    }
}
