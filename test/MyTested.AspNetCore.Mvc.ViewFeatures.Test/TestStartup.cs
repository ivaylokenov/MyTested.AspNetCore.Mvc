namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Microsoft.AspNetCore.Builder;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(TestStartup).GetTypeInfo().Assembly);
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}
