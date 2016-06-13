namespace MyTested.AspNetCore.Mvc.EntityFrameworkCore.Test
{
    using System.Linq;
    using Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Common;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void ReplaceDbContextShouldReplaceNonInMemoryDatabaseWithInMemoryScopedOne()
        {
            var services = new ServiceCollection();

            services.AddDbContext<CustomDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));

            services.ReplaceDbContext();

            this.AssertCorrectDbContextAndOptions(services);
        }

        [Fact]
        public void ReplaceDbContextShouldReplaceInMemoryDatabaseWithInMemoryScopedOne()
        {
            var services = new ServiceCollection();

            services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());

            services.ReplaceDbContext();

            this.AssertCorrectDbContextAndOptions(services);
        }

        [Fact]
        public void ReplaceDbContextShouldNotAddDbContextIfMissing()
        {
            var services = new ServiceCollection();
            
            services.ReplaceDbContext();

            var serviceProvider = services.BuildServiceProvider();

            Assert.Null(serviceProvider.GetService<DbContext>());
        }

        private void AssertCorrectDbContextAndOptions(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var dbContextService = services.FirstOrDefault(s => s.ServiceType == typeof(CustomDbContext));

            Assert.NotNull(dbContextService);
            Assert.Equal(ServiceLifetime.Scoped, dbContextService.Lifetime);

            var customDbContext = serviceProvider.GetService<CustomDbContext>();

            Assert.NotNull(customDbContext);

            var dbContextOptions = serviceProvider.GetService<DbContextOptions<CustomDbContext>>();

            Assert.NotNull(dbContextOptions);
            Assert.Equal(3, dbContextOptions.Extensions.Count());

            var coreOptionsExtension = dbContextOptions.FindExtension<CoreOptionsExtension>();
            var inMemoryOptionsExtension = dbContextOptions.FindExtension<InMemoryOptionsExtension>();
            var scopedInMemoryOptionsExtension = dbContextOptions.FindExtension<ScopedInMemoryOptionsExtension>();

            Assert.NotNull(coreOptionsExtension);
            Assert.NotNull(inMemoryOptionsExtension);
            Assert.NotNull(scopedInMemoryOptionsExtension);
        }
    }
}
