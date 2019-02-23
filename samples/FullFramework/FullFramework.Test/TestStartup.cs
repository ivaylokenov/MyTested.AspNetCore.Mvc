namespace FullFramework.Test
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using MyTested.AspNetCore.Mvc;
    using Web;
    using Web.Services;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceTransient<IDataService, DataServiceMock>();
        }
    }
}
