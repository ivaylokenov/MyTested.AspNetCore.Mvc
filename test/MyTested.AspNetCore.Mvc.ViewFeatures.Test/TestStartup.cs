namespace MyTested.AspNetCore.Mvc.ViewFeatures.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Test.Setups;
    using System.Reflection;
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
