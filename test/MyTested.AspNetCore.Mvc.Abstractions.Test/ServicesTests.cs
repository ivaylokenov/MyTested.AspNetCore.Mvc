namespace MyTested.AspNetCore.Mvc.Test
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Internal;
    using Internal.Application;
    using Internal.Contracts;
    using Internal.Routing;
    using Internal.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Routing.Constraints;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Services;
    using Setups.StartupFilters;
    using Setups.Startups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void UsesDefaultServicesShouldPopulateDefaultServices()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var markerService = TestServiceProvider.GetService(WebFramework.Internals.MvcMarkerService);

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
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithServiceProviderWhenTestServicesAreAddedByHand()
        {
            MyApplication.StartsFrom<CustomStartupWithBuiltProvider>();

            var injectedService = TestApplication.Services.GetService<IInjectedService>();
            var injectedServiceFromRoutingServices = TestApplication.RoutingServices.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<ReplaceableInjectedService>(injectedService);

            Assert.NotNull(injectedServiceFromRoutingServices);
            Assert.IsAssignableFrom<ReplaceableInjectedService>(injectedServiceFromRoutingServices);

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithCustomServiceProviderWhenTestServicesAreAddedByHand()
        {
            MyApplication.StartsFrom<CustomStartupWithCustomServiceProvider>();

            var injectedService = TestApplication.Services.GetService<IInjectedService>();
            var injectedServiceFromRoutingServices = TestApplication.RoutingServices.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<InjectedService>(injectedService);

            Assert.NotNull(injectedServiceFromRoutingServices);
            Assert.IsAssignableFrom<InjectedService>(injectedServiceFromRoutingServices);

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void IsUsingWithStartUpClassShouldThrowStartupExceptionWithServiceProviderWhenTestServicesAreMissing()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyApplication.StartsFrom<CustomStartupWithDefaultBuildProvider>();

                    TestServiceProvider.GetService<IInjectedService>();
                },
                "Testing services could not be resolved. If your ConfigureServices method returns an IServiceProvider, you should either change it to return 'void' or manually register the required testing services by calling one of the provided IServiceCollection extension methods in the 'MyTested.AspNetCore.Mvc' namespace. An easy way to do the second option is to add a TestStartup class at the root of your test project and invoke the extension methods there.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void IsUsingWithStartUpClassShouldThrowExceptionWithServiceProviderWhenTestServicesAreMissing()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyApplication
                        .StartsFrom<InvalidStartup>()
                        .WithConfiguration(configuration => configuration
                            .Add("General:Environment", "Invalid"));

                    TestServiceProvider.GetService<IInjectedService>();
                },
                "Testing services could not be resolved. If your ConfigureServices method returns an IServiceProvider, you should either change it to return 'void' or manually register the required testing services by calling one of the provided IServiceCollection extension methods in the 'MyTested.AspNetCore.Mvc' namespace.");

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
        public void IsUsingShouldRecreateServicesEveryTimeItIsInvoked()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var markerService = TestServiceProvider.GetService(WebFramework.Internals.MvcMarkerService);

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

            var service = TestServiceProvider.GetService(WebFramework.Internals.MvcMarkerService);

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

            var service = TestServiceProvider.GetService(WebFramework.Internals.MvcMarkerService);

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
        public void DefaultConfigAndAdditionalRoutesShouldSetOnlyThemWithoutEndpoints()
        {
            MyApplication
                .StartsFrom<NoEndpointsStartup>()
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
            MyApplication
                .StartsFrom<CustomStartup>()
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

            MyApplication
                .StartsFrom<CustomStartup>()
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
            MyApplication
                .StartsFrom<CustomStartup>()
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
            MyApplication
                .StartsFrom<DefaultStartup>()
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
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            // This call ensures services are loaded (uses lazy loading).
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
        public void DefaultServicesShouldBeRegisteredCorrectly()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            Assert.NotNull(TestServiceProvider.GetService<IHostEnvironment>());
            Assert.NotNull(TestServiceProvider.GetService<IHostApplicationLifetime>());
            Assert.NotNull(TestServiceProvider.GetService<IApplicationBuilderFactory>());
            Assert.NotNull(TestServiceProvider.GetService<IHttpContextFactory>());
            Assert.NotNull(TestServiceProvider.GetService<IMiddlewareFactory>());
            Assert.NotNull(TestServiceProvider.GetService<ILoggerFactory>());
            Assert.NotNull(TestServiceProvider.GetService<IConfiguration>());
            Assert.NotNull(TestServiceProvider.GetService<DiagnosticListener>());
            Assert.NotNull(TestServiceProvider.GetService<DiagnosticSource>());
            Assert.NotNull(TestServiceProvider.GetService<IStartupFilter>());
            Assert.NotNull(TestServiceProvider.GetService<IServiceProviderFactory<IServiceCollection>>());
            Assert.NotNull(TestServiceProvider.GetService<ObjectPoolProvider>());
            Assert.NotNull(TestServiceProvider.GetService<IRoutingServices>());
        }

        [Fact]
        public void MissingRoutingServicesShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyApplication
                        .StartsFrom<DefaultStartup>()
                        .WithServices(services => services.Remove<IRoutingServices>());

                    TestServiceProvider.GetService<IInjectedService>();
                },
                "No service for type 'MyTested.AspNetCore.Mvc.Internal.Contracts.IRoutingServices' has been registered.");

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void CustomRoutingServiceProviderShouldReplaceTheDefaultOne()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    var servicesClone = new ServiceCollection { services };

                    servicesClone.AddTransient<IInjectedService, InjectedService>();

                    var routingServices = new RoutingServices
                    {
                        ServiceProvider = servicesClone.BuildServiceProvider()
                    };

                    services.ReplaceSingleton<IRoutingServices>(routingServices);
                });

            var defaultService = TestApplication.Services.GetService<IInjectedService>();
            var routingService = TestApplication.RoutingServices.GetService<IInjectedService>();

            Assert.Null(defaultService);
            Assert.NotNull(routingService);

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void CustomRoutingServiceCollectionShouldReplaceTheDefaultOne()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    var servicesClone = new ServiceCollection { services };

                    servicesClone.AddTransient<IInjectedService, InjectedService>();

                    var routingServices = new RoutingServices
                    {
                        ServiceCollection = servicesClone
                    };

                    services.ReplaceSingleton<IRoutingServices>(routingServices);
                });

            var defaultService = TestApplication.Services.GetService<IInjectedService>();
            var routingService = TestApplication.RoutingServices.GetService<IInjectedService>();

            Assert.Null(defaultService);
            Assert.NotNull(routingService);

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void MissingRoutingServiceCollectionShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyApplication
                        .StartsFrom<DefaultStartup>()
                        .WithServices(services =>
                        {
                            var routingServices = new RoutingServices
                            {
                                ServiceCollection = null
                            };

                            services.ReplaceSingleton<IRoutingServices>(routingServices);
                        });

                    // This call ensures services are loaded (uses lazy loading).
                    var setupServices = TestApplication.Services;
                },
                "Route testing requires the registered IRoutingServices service implementation to provide test routing services by either the ServiceProvider property or the ServiceCollection one.");

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void FullConfigureStartupShouldInjectServicesCorrectly()
        {
            MyApplication.StartsFrom<FullConfigureStartup>();

            Assert.NotNull(TestServiceProvider.GetService<IHostEnvironment>());
            Assert.NotNull(TestServiceProvider.GetService<ILoggerFactory>());
            Assert.NotNull(TestServiceProvider.GetService<IHostApplicationLifetime>());

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void ConfigureContainerServicesShouldBeRegistered()
        {
            MyApplication
                .IsRunningOn(server => server
                    .WithServices(services => services
                        .AddTransient<IServiceProviderFactory<CustomContainer>, CustomContainerFactory>())
                    .WithStartup<CustomStartupWithConfigureContainer>());

            var injectedService = TestApplication.Services.GetService<IInjectedService>();
            var injectedServiceFromRouteServiceProvider = TestApplication.RoutingServices.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<InjectedService>(injectedService);

            Assert.NotNull(injectedServiceFromRouteServiceProvider);
            Assert.IsAssignableFrom<InjectedService>(injectedServiceFromRouteServiceProvider);

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void ConfigureContainerServicesShouldBeRegisteredFromStaticStartup()
        {
            MyApplication.StartsFrom<StaticStartup>();

            var injectedService = TestApplication.Services.GetService<IInjectedService>();
            var injectedServiceFromRouteServiceProvider = TestApplication.RoutingServices.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<InjectedService>(injectedService);

            Assert.NotNull(injectedServiceFromRouteServiceProvider);
            Assert.IsAssignableFrom<InjectedService>(injectedServiceFromRouteServiceProvider);

            MyApplication
                .IsRunningOn(server => server
                    .WithStartup<DefaultStartup>());
        }

        [Fact]
        public void ConfigureContainerWithNoServerServicesShouldThrowCorrectExceptionMessage()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyApplication.StartsFrom<CustomStartupWithDefaultConfigureContainer>();

                    // This call ensures services are loaded (uses lazy loading).
                    var setupServices = TestApplication.Services;
                },
                "No service for type 'Microsoft.Extensions.DependencyInjection.IServiceProviderFactory`1[MyTested.AspNetCore.Mvc.Test.Setups.Common.CustomContainer]' has been registered. Services could not be configured. If your web project is registering services outside of the Startup class (during the WebHost configuration in the Program.cs file for example), you should provide them to the test framework too by calling 'IsRunningOn(server => server.WithServices(servicesAction))'. Since this method should be called only once per test project, you may invoke it in the static constructor of your TestStartup class or if your test runner supports it - in the test assembly initialization.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void RegisteringServerServicesTwiceShouldResolveThemCorrectly()
        {
            MyApplication
                .IsRunningOn(server => server
                    .WithServices(services => services
                        .AddTransient<IInjectedService, InjectedService>())
                    .WithStartup<DefaultStartup>());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom<InjectedService>(injectedService);

            MyApplication.IsRunningOn(server => server
                .WithServices(services => services
                    .AddTransient<IAnotherInjectedService, AnotherInjectedService>())
                .WithStartup<DefaultStartup>());

            var secondInjectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.Null(secondInjectedService);
            Assert.NotNull(anotherInjectedService);
            Assert.IsAssignableFrom<AnotherInjectedService>(anotherInjectedService);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void LegacyRoutesShouldHaveAttributeRouteRegistered()
        {
            MyApplication.StartsFrom<NoEndpointsStartup>();

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);

            var attributeRoute = routes[0];

            Assert.NotNull(attributeRoute);
            Assert.Contains(nameof(Attribute), attributeRoute.GetType().Name);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void EndpointRoutesShouldHaveAttributeRouteRegistered()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);

            var attributeRoute = routes[0];

            Assert.NotNull(attributeRoute);
            Assert.Contains(nameof(Attribute), attributeRoute.GetType().Name);
        }

        [Fact]
        public void EndpointRoutesShouldRegisterCorrectValues()
        {
            MyApplication.StartsFrom<EndpointsRoutingStartup>();

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(3, routes.Count);

            var attributeRoute = routes[0];

            Assert.Contains(nameof(Attribute), attributeRoute.GetType().Name);

            var areaRoute = routes[1] as Route;

            Assert.NotNull(areaRoute); 
            Assert.Equal("files", areaRoute.Name);
            Assert.Equal("Files/{controller=Default}/{action=Test}/{fileName=None}", areaRoute.RouteTemplate);

            var areaRouteDefaults = areaRoute.Defaults;

            Assert.Equal(4, areaRouteDefaults.Count);

            var areaKey = "area";

            Assert.Contains(areaKey, areaRouteDefaults.Keys);
            Assert.Equal("Files", areaRouteDefaults[areaKey]);

            var normalRoute = routes[2] as Route;

            Assert.NotNull(normalRoute);
            Assert.Equal("test", normalRoute.Name);
            Assert.Equal("Test/{action=Index}/{id?}", normalRoute.RouteTemplate);

            var normalRouteDefaults = normalRoute.Defaults;
            var controllerKey = "controller";

            Assert.Contains(controllerKey, normalRouteDefaults.Keys);
            Assert.Equal("Test", normalRouteDefaults[controllerKey]);

            var normalRouteConstraints = normalRoute.Constraints;
            var idKey = "id";

            Assert.Contains(idKey, normalRouteConstraints.Keys);

            var optionalRouteConstraint = normalRouteConstraints[idKey] as OptionalRouteConstraint;

            Assert.NotNull(optionalRouteConstraint);

            var regexRouteConstraint = optionalRouteConstraint.InnerConstraint as RegexRouteConstraint;

            Assert.NotNull(regexRouteConstraint);
            Assert.Equal(@"^(\d)$", regexRouteConstraint.Constraint.ToString());

            var normalRouteDataTokens = normalRoute.DataTokens;

            Assert.Equal(2, normalRouteDataTokens.Count);

            var firstDataTokenKey = "random";
            var secondDataTokenKey = "another";

            Assert.Contains(firstDataTokenKey, normalRouteDataTokens.Keys);
            Assert.Equal("value", normalRouteDataTokens[firstDataTokenKey]);
            Assert.Contains(secondDataTokenKey, normalRouteDataTokens.Keys);
            Assert.Equal("token", normalRouteDataTokens[secondDataTokenKey]);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void StartupFiltersShouldBeRegisteredAndConsidered()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddSingleton<IStartupFilter>(new CustomStartupFilter()));

            var sameStartupFilter = TestServiceProvider.GetService<IStartupFilter>() as CustomStartupFilter;

            Assert.NotNull(sameStartupFilter);
            Assert.True(sameStartupFilter.ConfigurationRegistered);

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
