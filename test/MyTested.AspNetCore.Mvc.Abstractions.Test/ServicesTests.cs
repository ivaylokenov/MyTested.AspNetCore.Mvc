namespace MyTested.AspNetCore.Mvc.Test
{
    using System;
    using System.Linq;
    using Internal;
    using Internal.Application;
    using Internal.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Services;
    using Setups.Startups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void UsesDefaultServicesShouldPopulateDefaultServices()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);
        }

        [Fact]
        public void IsUsingShouldAddServices()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void IsUsingShouldAddServicesWithOptions()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var initialSetOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            MyApplication.StartsFrom<DefaultStartup>()
                .WithServices(TestObjectFactory.GetCustomServicesWithOptionsRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            var setOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            Assert.NotNull(setOptions);
            Assert.Equal(initialSetOptions.Count() + 1, setOptions.Count());

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithAction()
        {
            MyApplication.StartsFrom<CustomStartup>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<ReplaceableInjectedService>(injectedService);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithFunc()
        {
            MyApplication.StartsFrom<CustomStartupWithBuiltProvider>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<ReplaceableInjectedService>(injectedService);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void IsUsingWithAdditionalServicesShouldUseThem()
        {
            MyApplication
                .StartsFrom<CustomStartup>()
                .WithServices(services =>
                {
                    services.AddTransient<IInjectedService, InjectedService>();
                });

            var injectedServices = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedServices);
            Assert.IsAssignableFrom<InjectedService>(injectedServices);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void IsUsingWithWrongStartupClassShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyApplication.StartsFrom<MvcController>();
                    TestServiceProvider.GetService<IInjectedService>();
                },
                "A public method named 'ConfigureTest' or 'Configure' could not be found in the 'MyTested.AspNetCore.Mvc.Test.Setups.Controllers.MvcController' type.");
        }

        [Fact]
        public void IsUsingShouldRecreateServicesEverytimeItIsInvoked()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);

            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyApplication.StartsFrom<DefaultStartup>();

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    injectedService = TestServiceProvider.GetRequiredService<IInjectedService>();
                },
                "IInjectedService could not be resolved from the services provider. Before running this test case, the service should be registered in the 'StartsFrom' method and cannot be null.");

            MyApplication.StartsFrom<DefaultStartup>()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            injectedService = TestServiceProvider.GetService<IInjectedService>();
            anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DefaultConfigShouldSetMvc()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var service = TestServiceProvider.GetRequiredService<MvcMarkerService>();

            Assert.NotNull(service);
        }

        [Fact]
        public void DefaultConfigShouldSetDefaultRoutes()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);
        }

        [Fact]
        public void DefaultConfigAndAdditionalServicesShouldWorkCorrectly()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMvc();
                });

            var service = TestServiceProvider.GetRequiredService<MvcMarkerService>();

            Assert.NotNull(service);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DefaultConfigAndAdditionalApplicationShouldWorkCorrectly()
        {
            var set = false;

            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithConfiguration(app =>
                {
                    set = true;
                });

            Assert.NotNull(TestApplication.Services);
            Assert.True(set);
        }

        [Fact]
        public void DefaultConfigAndAdditionalRoutesShouldSetOnlyThem()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithRoutes(routes =>
                {
                    routes.MapRoute(
                        name: "another",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

            var setRoutes = TestApplication.Router as RouteCollection;

            Assert.NotNull(setRoutes);
            Assert.Equal(3, setRoutes.Count);
        }

        [Fact]
        public void CustomStartupShouldSetServicesAndRoutesCorrectly()
        {
            MyApplication.StartsFrom<CustomStartup>();

            var service = TestApplication.Services.GetRequiredService<IInjectedService>();

            Assert.NotNull(service);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CustomStartupWithAdditionalServiceShouldSetThem()
        {
            MyApplication.StartsFrom<CustomStartup>()
                .WithServices(services =>
                {
                    services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                });

            var service = TestApplication.Services.GetRequiredService<IAnotherInjectedService>();

            Assert.NotNull(service);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CustomStartupWithAdditionalApplicationShouldWorkCorrectly()
        {
            var set = false;

            MyApplication.StartsFrom<CustomStartup>()
                .WithConfiguration(app =>
                {
                    set = true;
                });

            var service = TestApplication.Services.GetRequiredService<IInjectedService>();

            Assert.NotNull(service);

            Assert.True(set);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithCustomStartupAndAdditionalRoutesShouldWorkCorrectly()
        {
            MyApplication.StartsFrom<CustomStartup>()
                .WithRoutes(routes =>
                {
                    routes.MapRoute(
                        name: "another",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

            var service = TestApplication.Services.GetRequiredService<IInjectedService>();

            Assert.NotNull(service);

            var routesCollection = TestApplication.Router as RouteCollection;

            Assert.NotNull(routesCollection);
            Assert.Equal(3, routesCollection.Count);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithoutHttpContextFactoryTheDefaultMockHttpContextShouldBeProvided()
        {
            MyApplication.StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.RemoveTransient<IHttpContextFactory>();
                });

            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();

            Assert.Null(httpContextFactory);

            var httpContext = TestHelper.CreateHttpContextMock();

            Assert.NotNull(httpContext);
            Assert.Equal(ContentType.FormUrlEncoded, httpContext.Request.ContentType);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithHttpContextFactoryShouldReturnMockHttpContextBasedOnTheFactoryCreatedHttpContext()
        {
            MyApplication.StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.ReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                });

            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();

            Assert.NotNull(httpContextFactory);

            var httpContext = TestHelper.CreateHttpContextMock();

            Assert.NotNull(httpContext);
            Assert.Equal(ContentType.AudioVorbis, httpContext.Request.ContentType);

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void MockMemoryCacheShouldNotBeRegisteredIfNoCacheIsAdded()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.RemoveSingleton<IMemoryCache>());

            Assert.Null(TestServiceProvider.GetService<IMemoryCache>());

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void CustomTempDataProviderShouldOverrideTheMockOne()
        {
            MyApplication.StartsFrom<DataStartup>();

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<CustomTempDataProvider>(tempDataProvider);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ServiceLifeTimesShouldBeSavedCorrectly()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddScoped<IScopedService, ScopedService>();
                    services.AddSingleton<IInjectedService, InjectedService>();
                    services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                });

            // this call ensures services are loaded (uses lazy loading)
            var setupServices = TestApplication.Services;

            Assert.NotNull(setupServices.GetService<IScopedService>());
            Assert.NotNull(setupServices.GetService<IInjectedService>());
            Assert.NotNull(setupServices.GetService<IAnotherInjectedService>());

            var scopedServiceLifetime = TestServiceProvider.GetServiceLifetime(typeof(IScopedService));
            Assert.Equal(ServiceLifetime.Scoped, scopedServiceLifetime);

            var singletonServiceLifetime = TestServiceProvider.GetServiceLifetime<IInjectedService>();
            Assert.Equal(ServiceLifetime.Singleton, singletonServiceLifetime);

            var transientServiceLifetime = TestServiceProvider.GetServiceLifetime<IAnotherInjectedService>();
            Assert.Equal(ServiceLifetime.Transient, transientServiceLifetime);
            
            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void FullConfigureStartupShouldInjectServicesCorrectly()
        {
            MyApplication.StartsFrom<FullConfigureStartup>();
            
            Assert.NotNull(TestServiceProvider.GetService<IHostingEnvironment>());
            Assert.NotNull(TestServiceProvider.GetService<ILoggerFactory>());
            Assert.NotNull(TestServiceProvider.GetService<IApplicationLifetime>());

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
