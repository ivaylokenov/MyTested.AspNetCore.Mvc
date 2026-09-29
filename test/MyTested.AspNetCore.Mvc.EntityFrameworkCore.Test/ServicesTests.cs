namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Linq;
    using System.Threading.Tasks;
    using Internal.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Xunit;

    public class ServicesTests
    {
        private const string TestConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;";

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
        public void ReplaceDbContextWithInterfaceShouldReplaceDbContextCorrectly()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ICustomDbContext, CustomDbContext>(options => options.UseSqlServer(TestConnectionString));

            services.ReplaceDbContext();

            var baseDbContextService = services.FirstOrDefault(s => s.ServiceType == typeof(DbContext));
            var classDbContextService = services.FirstOrDefault(s => s.ServiceType == typeof(CustomDbContext));
            var interfaceDbContextService = services.FirstOrDefault(s => s.ServiceType == typeof(ICustomDbContext));

            Assert.NotNull(baseDbContextService);
            Assert.NotNull(classDbContextService);
            Assert.NotNull(interfaceDbContextService);

            Assert.Equal(ServiceLifetime.Scoped, baseDbContextService.Lifetime);
            Assert.Equal(ServiceLifetime.Scoped, classDbContextService.Lifetime);
            Assert.Equal(ServiceLifetime.Scoped, interfaceDbContextService.Lifetime);

            var serviceProvider = services.BuildServiceProvider();

            var baseDbContext = serviceProvider.GetService<DbContext>();
            var classDbContext = serviceProvider.GetService<CustomDbContext>();
            var interfaceDbContext = serviceProvider.GetService<ICustomDbContext>();

            Assert.NotNull(baseDbContext);
            Assert.NotNull(classDbContext);
            Assert.NotNull(interfaceDbContext);

            baseDbContext.Add(new CustomModel());
            baseDbContext.SaveChanges();

            baseDbContext = serviceProvider.GetService<DbContext>();
            classDbContext = serviceProvider.GetService<CustomDbContext>();
            interfaceDbContext = serviceProvider.GetService<ICustomDbContext>();

            Assert.Single(baseDbContext.Set<CustomModel>());
            Assert.Single(classDbContext.Models);
            Assert.Single(interfaceDbContext.Models);
        }

        [Fact]
        public void CallingMigrateShouldNotThrowExceptionWithInMemoryDatabase()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();
            
            services.BuildServiceProvider().GetRequiredService<CustomDbContext>().Database.Migrate();
        }

        [Fact]
        public async Task CallingMigrateAsyncShouldNotThrowExceptionWithInMemoryDatabase()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();

            var database = services.BuildServiceProvider().GetRequiredService<CustomDbContext>().Database;

            await database.MigrateAsync();
            await database.MigrateAsync("TargetMigration");
        }

        [Fact]
        public void HasPendingModelChangesShouldReturnFalseWithInMemoryDatabase()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();

            var database = services.BuildServiceProvider().GetRequiredService<CustomDbContext>().Database;

            Assert.False(database.HasPendingModelChanges());
        }

        [Fact]
        public void ReplaceDbContextShouldRegisterMigratorMock()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();

            var dbContext = services.BuildServiceProvider().GetRequiredService<CustomDbContext>();

            var migrator = Assert.IsType<MigratorMock>(dbContext.GetService<IMigrator>());

            Assert.Empty(migrator.GenerateScript());
        }

        [Fact]
        public void ReplaceDbContextShouldIsolateDataBetweenScopes()
        {
            var services = new ServiceCollection();

            this.AddDbContextWithSqlServer(services);

            services.ReplaceDbContext();

            using var serviceProvider = services.BuildServiceProvider();
            using var firstScope = serviceProvider.CreateScope();
            using var secondScope = serviceProvider.CreateScope();

            var firstDbContext = firstScope.ServiceProvider.GetRequiredService<CustomDbContext>();
            var secondDbContext = secondScope.ServiceProvider.GetRequiredService<CustomDbContext>();

            firstDbContext.Models.Add(new CustomModel { Id = 1, Name = "First" });
            firstDbContext.SaveChanges();

            Assert.Single(firstDbContext.Models);
            Assert.Empty(secondDbContext.Models);

            secondDbContext.Models.Add(new CustomModel { Id = 1, Name = "Second" });
            secondDbContext.SaveChanges();

            Assert.Equal("First", Assert.Single(firstDbContext.Models.AsNoTracking()).Name);
            Assert.Equal("Second", Assert.Single(secondDbContext.Models.AsNoTracking()).Name);

            using var thirdScope = serviceProvider.CreateScope();

            Assert.Empty(thirdScope.ServiceProvider.GetRequiredService<CustomDbContext>().Models);
        }

        private void AddDbContextWithSqlServer(IServiceCollection services)
            => this.AddDbContextWithSqlServer<CustomDbContext>(services);

        private void AddDbContextWithSqlServer<TDbContext>(IServiceCollection services)
            where TDbContext : DbContext
            => services.AddDbContext<TDbContext>(options => options.UseSqlServer(TestConnectionString));

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

#pragma warning disable EF1001 // Internal EF Core API usage.
            var coreOptionsExtension = dbContextOptions.FindExtension<CoreOptionsExtension>();
            var inMemoryOptionsExtension = dbContextOptions.FindExtension<InMemoryOptionsExtension>();
            var scopedInMemoryOptionsExtension = dbContextOptions.FindExtension<ScopedInMemoryOptionsExtension>();
#pragma warning restore EF1001 // Internal EF Core API usage.

            Assert.NotNull(coreOptionsExtension);
            Assert.NotNull(inMemoryOptionsExtension);
            Assert.NotNull(scopedInMemoryOptionsExtension);
        }
    }
}
