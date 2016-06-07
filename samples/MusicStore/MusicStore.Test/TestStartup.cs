namespace MusicStore.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.Storage.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Models;
    using MyTested.AspNetCore.Mvc;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
            : base(env)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            
            services.Remove<DbContextOptions<MusicStoreContext>>();
            services.AddDbContext<MusicStoreContext>(opts =>
            {
                opts.UseInMemoryDatabase();

                ((IDbContextOptionsBuilderInfrastructure)opts).AddOrUpdateExtension(new ScopedInMemoryOptionsExtension());
            });
            
            services.ReplaceSingleton<SignInManager<ApplicationUser>, MockedSignInManager>();
        }

        private class ScopedInMemoryOptionsExtension : InMemoryOptionsExtension
        {
            public override void ApplyServices(IServiceCollection services)
            {
                services.Replace<IInMemoryStore, InMemoryStore>(ServiceLifetime.Scoped);
            }
        }
    }
}
