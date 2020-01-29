namespace Lite.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using Web;
    using Web.Services;

    public class TestStartup : Startup
    {
        public TestStartup(IWebHostEnvironment env)
            : base(env)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.Replace<IData>(sp => Mocks.GetData(), ServiceLifetime.Scoped);
        }
    }
}
