namespace MyTested.AspNetCore.Mvc.Options.Test
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Test.Setups;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(TestStartup).GetTypeInfo().Assembly);
        }
    }
}
