namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Linq;
    using Internal.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void ReplaceDbContextShouldReplaceNonInMemoryDatabaseWithInMemoryScopedOne()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();

            this.AssertCorrectDbContextAndOptions(services);
        }

        [Fact]
        public void ReplaceDbContextShouldReplaceInMemoryDatabaseWithInMemoryScopedOne()
        {
            var services = new ServiceCollection();

            services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));

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

        [Fact]
        public void ReplaceDbContextShouldReplaceMultipleDbContextTypes()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer<CustomDbContext>(services);
            this.AddDbContextWithSqlServer<AnotherDbContext>(services);

            services.ReplaceDbContext();

            this.AssertCorrectDbContextAndOptions<CustomDbContext>(services);
            this.AssertCorrectDbContextAndOptions<AnotherDbContext>(services);
        }

        [Fact]
        public void CallingMigrateShouldNotThrowExceptionWithInMemoryDatabase()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();
            
            services.BuildServiceProvider().GetRequiredService<CustomDbContext>().Database.Migrate();
        }

        private void AddDbContextWithSqlServer(IServiceCollection services)
            => this.AddDbContextWithSqlServer<CustomDbContext>(services);

        private void AddDbContextWithSqlServer<TDbContext>(IServiceCollection services)
            where TDbContext : DbContext
            => services.AddDbContext<TDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));

        private void AssertCorrectDbContextAndOptions(IServiceCollection services)
            => this.AssertCorrectDbContextAndOptions<CustomDbContext>(services);

        private void AssertCorrectDbContextAndOptions<TDbContext>(IServiceCollection services)
            where TDbContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();

            var dbContextService = services.FirstOrDefault(s => s.ServiceType == typeof(TDbContext));

            Assert.NotNull(dbContextService);
            Assert.Equal(ServiceLifetime.Scoped, dbContextService.Lifetime);

            var customDbContext = serviceProvider.GetService<TDbContext>();

            Assert.NotNull(customDbContext);

            var dbContextOptions = serviceProvider.GetService<DbContextOptions<TDbContext>>();

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
