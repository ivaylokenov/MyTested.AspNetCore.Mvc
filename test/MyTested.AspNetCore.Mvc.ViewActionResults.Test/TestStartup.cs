namespace MyTested.AspNetCore.Mvc.ViewActionResults.Test
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class TestStartup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void ConfigureTest(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
