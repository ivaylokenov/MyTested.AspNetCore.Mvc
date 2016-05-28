namespace MusicStore.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Models;
    using MyTested.Mvc;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
            : base(env)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDbContext<MusicStoreContext>(opts => opts.UseInMemoryDatabase());
            services.ReplaceSingleton<SignInManager<ApplicationUser>, MockedSignInManager>();
        }
    }
}
