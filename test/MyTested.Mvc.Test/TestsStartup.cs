namespace MyTested.Mvc.Test
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

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
