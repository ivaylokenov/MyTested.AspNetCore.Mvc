namespace ApplicationParts.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Web;
    using Web.Data;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
            : base(env)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDbContext<ApplicationDbContext>(opts => opts.UseInMemoryDatabase());
        }
    }
}
