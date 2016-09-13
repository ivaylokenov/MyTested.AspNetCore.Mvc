namespace MyTested.AspNetCore.Mvc.ViewComponents.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Test.Setups;
    using System.Reflection;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(TestStartup).GetTypeInfo().Assembly);
        }
    }
}
