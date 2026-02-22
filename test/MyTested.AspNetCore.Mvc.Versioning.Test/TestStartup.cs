namespace MyTested.AspNetCore.Mvc.Test
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;

    public class TestStartup : DefaultStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Custom-Version"),
                    new UrlSegmentApiVersionReader());
            }).AddMvc();
        }
    }
}
