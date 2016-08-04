namespace MyTested.AspNetCore.Mvc.Http.Test
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
