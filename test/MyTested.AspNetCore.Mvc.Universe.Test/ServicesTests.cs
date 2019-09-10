namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal.Caching;
    using Internal.EntityFrameworkCore;
    using Internal.Services;
    using Internal.Session;
    using Microsoft.AspNetCore.Session;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using System.Linq;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void AddMvcUniverseTestingShouldReplaceDefaultMemoryCacheWithMockedVersion()
        {
            var services = new ServiceCollection();

            services.AddMvc();
            var defaultMemoryCache = services.BuildServiceProvider().GetService<IMemoryCache>();

            services.AddMvcUniverseTesting();
            var mockMemoryCache = services.BuildServiceProvider().GetService<IMemoryCache>();

            Assert.NotNull(defaultMemoryCache);
            Assert.NotNull(mockMemoryCache);
            Assert.NotSame(mockMemoryCache, defaultMemoryCache);
            Assert.IsAssignableFrom<IMemoryCache>(defaultMemoryCache);
            Assert.IsAssignableFrom<IMemoryCache>(mockMemoryCache);
            Assert.IsAssignableFrom<MemoryCacheMock>(mockMemoryCache);
        }

        [Fact]
        public void AddMvcUniverseTestingWithoutMemoryCacheShouldReplaceItWithMockedVersion()
        {
            var services = new ServiceCollection();

            services.AddMvc();
            services.Remove<IMemoryCache>();
            var defaultMemoryCache = services.BuildServiceProvider().GetService<IMemoryCache>();

            services.AddMvcUniverseTesting();
            var mockMemoryCache = services.BuildServiceProvider().GetService<IMemoryCache>();

            Assert.Null(defaultMemoryCache);
            Assert.NotNull(mockMemoryCache);
            Assert.IsAssignableFrom<IMemoryCache>(mockMemoryCache);
            Assert.IsAssignableFrom<MemoryCacheMock>(mockMemoryCache);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldOvverideDefaultMemoryCacheWithMockedVersion()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddMvcUniverseTesting();
                });

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MemoryCacheMock>(memoryCache);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void AddMvcUniverseTestingShouldReplaceDefaultSessionStoreWithMockedVersion()
        {
            var services = new ServiceCollection();
            services.AddMvc();
            services.AddTransient<ISessionStore, CustomSessionStore>();

            var defaultSessionStore = services.BuildServiceProvider().GetService<ISessionStore>();

            services.AddMvcUniverseTesting();
            var mockSessionStore= services.BuildServiceProvider().GetService<ISessionStore>();

            Assert.NotNull(defaultSessionStore);
            Assert.NotNull(mockSessionStore);
            Assert.NotSame(mockSessionStore, defaultSessionStore);
            Assert.IsAssignableFrom<ISessionStore>(defaultSessionStore);
            Assert.IsAssignableFrom<ISessionStore>(mockSessionStore);
            Assert.IsAssignableFrom<SessionStoreMock>(mockSessionStore);
        }

        [Fact]
        public void AddMvcUniverseTestingWithoutSessionStoreShouldReplaceItWithMockedVersion()
        {
            var services = new ServiceCollection();
            services.AddMvc();

            var defaultSessionStore = services.BuildServiceProvider().GetService<ISessionStore>();

            services.AddMvcUniverseTesting();
            var mockSessionStore = services.BuildServiceProvider().GetService<ISessionStore>();

            Assert.Null(defaultSessionStore);
            Assert.NotNull(mockSessionStore);
            Assert.IsAssignableFrom<ISessionStore>(mockSessionStore);
            Assert.IsAssignableFrom<SessionStoreMock>(mockSessionStore);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldOvverideNullSessionStoreWithMockedVersion()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddMvcUniverseTesting();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<SessionStoreMock>(session);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void AddMvcUniverseTestingShouldReplaceNonInMemoryDatabaseWithInMemoryScopedOne()
        {
            var services = new ServiceCollection();

            services.AddMvc();
            services.AddDbContext<CustomDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));

            services.AddMvcUniverseTesting();

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
