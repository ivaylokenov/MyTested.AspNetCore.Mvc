namespace MyTested.Mvc.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;

    public class TestsStartup
    {
        public void ConfigureTestsServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void ConfigureTests(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
