namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(TestStartup).GetTypeInfo().Assembly);
        }
    }
}
