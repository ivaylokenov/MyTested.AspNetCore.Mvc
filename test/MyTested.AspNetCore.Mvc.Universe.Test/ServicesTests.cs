namespace MyTested.AspNetCore.Mvc.Test
{
    using System;
    using System.Linq;
    using Internal;
    using Internal.Caching;
    using Internal.Contracts;
    using Internal.EntityFrameworkCore;
    using Internal.Formatters;
    using Internal.Routing;
    using Internal.Services;
    using Internal.Session;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Session;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Common;
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
            Assert.Contains(services, s => s.ServiceType == typeof(IMemoryCache) && s.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void AddMvcUniverseTestingWithoutMemoryCacheShouldAddMockedVersion()
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
            Assert.Contains(services, s => s.ServiceType == typeof(IMemoryCache) && s.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldOverrideDefaultMemoryCacheWithMockedVersion()
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
        public void AddMvcUniverseTestingShouldReplaceCustomSessionStoreWithMockedVersion()
        {
            var services = new ServiceCollection();

            services.AddMvc();
            services.AddTransient<ISessionStore, CustomSessionStore>();

            var customSessionStore = services.BuildServiceProvider().GetService<ISessionStore>();

            services.AddMvcUniverseTesting();
            var mockSessionStore= services.BuildServiceProvider().GetService<ISessionStore>();

            Assert.NotNull(customSessionStore);
            Assert.NotNull(mockSessionStore);
            Assert.NotSame(mockSessionStore, customSessionStore);
            Assert.IsAssignableFrom<ISessionStore>(customSessionStore);
            Assert.IsAssignableFrom<ISessionStore>(mockSessionStore);
            Assert.IsAssignableFrom<SessionStoreMock>(mockSessionStore);
        }

        [Fact]
        public void AddMvcUniverseTestingWithoutSessionStoreShouldAddMockedVersion()
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
        public void AddMvcUniverseTestingShouldOverrideNullSessionStoreWithMockedVersion()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddMvcUniverseTesting();
                });

            var sessionStore = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(sessionStore);
            Assert.IsAssignableFrom<SessionStoreMock>(sessionStore);

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

        [Fact]
        public void AddMvcUniverseTestingShouldReplaceOptionsWithScopedOnes()
        {
            var services = new ServiceCollection();

            services.AddMvc();

            Assert.Contains(services, s => s.ServiceType == typeof(IOptions<>) && s.Lifetime == ServiceLifetime.Singleton);

            services.AddMvcUniverseTesting();

            Assert.Contains(services, s => s.ServiceType == typeof(IOptions<>) && s.Lifetime == ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldAddStringInputFormatter()
        {
            MyApplication.StartsFrom<TestStartup>();

            var builtInOptions = TestServiceProvider.GetService<IOptions<MvcOptions>>();
            builtInOptions.Value.InputFormatters.RemoveType<StringInputFormatter>();

            Assert.NotNull(builtInOptions);
            Assert.True(builtInOptions.Value.InputFormatters.Count == 1);
            Assert.DoesNotContain(typeof(StringInputFormatter), builtInOptions.Value.InputFormatters.Select(f => f.GetType()));

            MyApplication.StartsFrom<TestStartup>()
                .WithServices(services => services.AddMvcUniverseTesting());

            builtInOptions = TestServiceProvider.GetService<IOptions<MvcOptions>>();

            Assert.NotNull(builtInOptions);
            Assert.True(builtInOptions.Value.InputFormatters.Count == 2);
            Assert.Contains(typeof(StringInputFormatter), builtInOptions.Value.InputFormatters.Select(f => f.GetType()));
        }

        [Fact]
        public void AddMvcUniverseTestingWithStringInputFormatterShouldNotOverrideIt()
        {
            var inputFormatter = new StringInputFormatter();

            MyApplication.StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.Configure<MvcOptions>(options =>
                    {
                        options.InputFormatters.Add(inputFormatter);
                    });

                    services.AddMvcUniverseTesting();
                });

            var builtInOptions = TestServiceProvider.GetService<IOptions<MvcOptions>>();

            Assert.NotNull(builtInOptions);
            Assert.True(builtInOptions.Value.InputFormatters.Count == 2);
            Assert.Contains(typeof(StringInputFormatter), builtInOptions.Value.InputFormatters.Select(f => f.GetType()));
            Assert.Same(inputFormatter, builtInOptions.Value.InputFormatters.FirstOrDefault(f => f.GetType() == typeof(StringInputFormatter)));
        }

        [Fact]
        public void AddMvcUniverseTestingShouldReplaceTempDataProviderWithMockedVersion()
        {
            MyApplication.StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.Replace<ITempDataProvider, CustomTempDataProvider>(ServiceLifetime.Scoped);
                });

            var tempDataPovider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataPovider);
            Assert.True(typeof(CustomTempDataProvider) == tempDataPovider.GetType());

            MyApplication.StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddMvcUniverseTesting();
                });

            tempDataPovider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataPovider);
            Assert.True(typeof(TempDataProviderMock) == tempDataPovider.GetType());
        }

        [Fact]
        public void AddMvcUniverseTestingWithoutTempDataProviderShouldAddMockedVersion()
        {
            var services = new ServiceCollection();

            services.AddMvc();
            services.Remove<ITempDataProvider>();

            var defaultTempDataProvider = services.BuildServiceProvider().GetService<ITempDataProvider>();

            services.AddMvcUniverseTesting();
            var mockTempDataProvider = services.BuildServiceProvider().GetService<ITempDataProvider>();

            Assert.Null(defaultTempDataProvider);
            Assert.NotNull(mockTempDataProvider);
            Assert.IsAssignableFrom<ITempDataProvider>(mockTempDataProvider);
            Assert.IsAssignableFrom<TempDataProviderMock>(mockTempDataProvider);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldAddViewComponentTesting()
        {
            var services = new ServiceCollection();

            services.AddMvc();

            var viewComponentPropertyActivator = services.BuildServiceProvider().GetService<IViewComponentPropertyActivator>();
            var viewComponentDescriptorCache = services.BuildServiceProvider().GetService<IViewComponentDescriptorCache>();

            Assert.Null(viewComponentPropertyActivator);
            Assert.Null(viewComponentDescriptorCache);

            services.AddMvcUniverseTesting();

            viewComponentPropertyActivator = services.BuildServiceProvider().GetService<IViewComponentPropertyActivator>();
            viewComponentDescriptorCache = services.BuildServiceProvider().GetService<IViewComponentDescriptorCache>();

            Assert.NotNull(viewComponentPropertyActivator);
            Assert.NotNull(viewComponentDescriptorCache);

            Assert.Contains(services, s => s.ServiceType == typeof(IViewComponentPropertyActivator) && s.Lifetime == ServiceLifetime.Singleton);
            Assert.Contains(services, s => s.ServiceType == typeof(IViewComponentDescriptorCache) && s.Lifetime == ServiceLifetime.Singleton);

            Assert.IsAssignableFrom<IViewComponentPropertyActivator>(viewComponentPropertyActivator);
            Assert.IsAssignableFrom<IViewComponentDescriptorCache>(viewComponentDescriptorCache);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldAddControllersTestingServices()
        {
            var services = new ServiceCollection();

            services.AddMvc();

            var validControllersCache = services.BuildServiceProvider().GetService<IValidControllersCache>();

            Assert.Null(validControllersCache);

            services.AddMvcUniverseTesting();
            validControllersCache = services.BuildServiceProvider().GetService<IValidControllersCache>();

            Assert.NotNull(validControllersCache);
            Assert.IsAssignableFrom<IValidControllersCache>(validControllersCache);
            Assert.Contains(services, s => s.ServiceType == typeof(IValidControllersCache) && s.Lifetime == ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldAddControllersTestingServicesAndConfigureConventions()
        {
            MyApplication.StartsFrom<TestStartup>();

            var mvcOptions = TestServiceProvider.GetService<IOptions<MvcOptions>>();

            Assert.NotNull(mvcOptions);
            Assert.NotEmpty(mvcOptions.Value.Conventions);
            Assert.True(mvcOptions.Value.Conventions.Count == 1);

            MyApplication.StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddMvcUniverseTesting();
                });

            var validControllersCache = TestServiceProvider.GetService<IValidControllersCache>();

            Assert.NotNull(validControllersCache);
            Assert.IsAssignableFrom<IValidControllersCache>(validControllersCache);

            mvcOptions = TestServiceProvider.GetService<IOptions<MvcOptions>>();

            Assert.NotNull(mvcOptions);
            Assert.NotEmpty(mvcOptions.Value.Conventions);
            Assert.True(mvcOptions.Value.Conventions.Count == 2);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldAddRoutingTestingServices()
        {
            var services = new ServiceCollection();

            services.AddMvc();

            var routingServices = services.BuildServiceProvider().GetService<IRoutingServices>();

            Assert.Null(routingServices);

            services.AddMvcUniverseTesting();
            routingServices = services.BuildServiceProvider().GetService<IRoutingServices>();

            Assert.NotNull(routingServices);
            Assert.IsAssignableFrom<IRoutingServices>(routingServices);
            Assert.Contains(services, s => s.ServiceType == typeof(IRoutingServices) && s.Lifetime == ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddMvcUniverseTestingWithRoutingTestingServicesShouldNotOverrideThem()
        {
            IRoutingServices routingServices = new RoutingServices();

            var services = new ServiceCollection();

            services.AddMvc();
            services.AddSingleton(typeof(IRoutingServices), routingServices);

            Assert.NotNull(services.BuildServiceProvider().GetService<IRoutingServices>());

            services.AddMvcUniverseTesting();
            var actualRoutingServices = services.BuildServiceProvider().GetService<IRoutingServices>();

            Assert.NotNull(routingServices);
            Assert.IsAssignableFrom<IRoutingServices>(routingServices);
            Assert.Same(routingServices, actualRoutingServices);
        }

        [Fact]
        public void AddMvcUniverseTestingShouldAddRoutingTestingServicesAndDisableEndPointRouting()
        {
            MyApplication.StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddMvcUniverseTesting();
                });

            var mvcOptions = TestServiceProvider.GetService<IOptions<MvcOptions>>();

            Assert.True(mvcOptions.Value.EnableEndpointRouting);
        }

        [Fact]
        public void WithoutServiceCollectionShouldThrowException()
        {
            IServiceCollection services = null;

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    services.AddMvcUniverseTesting();
                },
                "serviceCollection cannot be null.");
        }
    }
}
