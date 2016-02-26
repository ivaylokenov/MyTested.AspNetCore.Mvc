using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MyTested.Mvc.Tests
{
    using System;
    using System.Linq;
    using Exceptions;
    using Internal.Application;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Services;
    using Setups.Startups;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Internal.Contracts;
    using Internal.Routes;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.AspNetCore.Http;
    using Internal.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Internal;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Internal.Caching;
    using Internal;
    public class MyMvcTests
    {
        [Fact]
        public void UsesDefaultServicesShouldPopulateDefaultServices()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);
        }

        [Fact]
        public void IsUsingShouldAddServices()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingShouldAddServicesWithOptions()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var initialSetOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            MyMvc.IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesWithOptionsRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            var setOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            Assert.NotNull(setOptions);
            Assert.Equal(initialSetOptions.Count() + 1, setOptions.Count());

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithAction()
        {
            MyMvc.StartsFrom<CustomStartup>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithFunc()
        {
            MyMvc.StartsFrom<CustomStartupWithBuiltProvider>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingWithAdditionalServicesShouldUseThem()
        {
            MyMvc
                .StartsFrom<CustomStartup>()
                .WithServices(services =>
                {
                    services.AddTransient<IInjectedService, InjectedService>();
                });

            var injectedServices = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedServices);
            Assert.IsAssignableFrom(typeof(InjectedService), injectedServices);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingWithWrongStartupClassShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc.StartsFrom<MvcController>();
                    TestServiceProvider.GetService<IInjectedService>();
                },
                "A public method named 'ConfigureTests' or 'Configure' could not be found in the 'MyTested.Mvc.Tests.Setups.Controllers.MvcController' type.");
        }

        [Fact]
        public void IsUsingShouldRecreateServicesEverytimeItIsInvoked()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);

            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultConfiguration();

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    injectedService = TestServiceProvider.GetRequiredService<IInjectedService>();
                },
                "IInjectedService could not be resolved from the services provider. Before running this test case, the service should be registered in the 'StartsFrom' method and cannot be null.");

            MyMvc.IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            injectedService = TestServiceProvider.GetService<IInjectedService>();
            anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ControllerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyMvc.Controller<MvcController>().AndProvideTheController();

            Assert.NotNull(controller);
            Assert.IsAssignableFrom<MvcController>(controller);
        }

        [Fact]
        public void ControllerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyMvc.Controller(() => new MvcController(new InjectedService())).AndProvideTheController();

            Assert.NotNull(controller);
            Assert.IsAssignableFrom<MvcController>(controller);

            Assert.NotNull(controller.InjectedService);
            Assert.IsAssignableFrom<InjectedService>(controller.InjectedService);
        }

        [Fact]
        public void ControllerWithProvidedInstanceShouldPopulateCorrectInstanceOfControllerType()
        {
            var instance = new MvcController();
            var controller = MyMvc.Controller(instance).AndProvideTheController();

            Assert.NotNull(controller);
            Assert.IsAssignableFrom<MvcController>(controller);
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndWithRegisteredServicesShouldPopulateCorrectInstanceOfControllerType()
        {
            MyMvc.IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddTransient<IInjectedService, InjectedService>();
                    services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                });

            var controller = MyMvc.Controller<NoParameterlessConstructorController>().AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.Service);
            Assert.IsAssignableFrom<InjectedService>(controller.Service);
            Assert.NotNull(controller.AnotherInjectedService);
            Assert.IsAssignableFrom<AnotherInjectedService>(controller.AnotherInjectedService);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndNoServicesShouldThrowProperException()
        {
            MyMvc.IsUsingDefaultConfiguration();

            Test.AssertException<UnresolvedDependenciesException>(
                () =>
                {
                    MyMvc
                        .Controller<NoParameterlessConstructorController>()
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking no parameters.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigShouldSetMvc()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var service = TestServiceProvider.GetRequiredService<MvcMarkerService>();

            Assert.NotNull(service);
        }

        [Fact]
        public void DefaultConfigShouldSetDefaultRoutes()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);
        }

        [Fact]
        public void DefaultConfigAndAdditionalServicesShouldWorkCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMvc();
                });

            var service = TestServiceProvider.GetRequiredService<MvcMarkerService>();

            Assert.NotNull(service);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigAndAdditionalApplicationShouldWorkCorrectly()
        {
            var set = false;

            MyMvc
                .IsUsingDefaultConfiguration()
                .WithApplication(app =>
                {
                    set = true;
                });

            Assert.NotNull(TestApplication.Services);
            Assert.True(set);
        }

        [Fact]
        public void DefaultConfigAndAdditionalRoutesShouldSetOnlyThem()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
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
            MyMvc.StartsFrom<CustomStartup>();

            var service = TestApplication.Services.GetRequiredService<IInjectedService>();

            Assert.NotNull(service);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CustomStartupWithAdditionalServiceShouldSetThem()
        {
            MyMvc.StartsFrom<CustomStartup>()
                .WithServices(services =>
                {
                    services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                });

            var service = TestApplication.Services.GetRequiredService<IAnotherInjectedService>();

            Assert.NotNull(service);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CustomStartupWithAdditionalApplicationShouldWorkCorrectly()
        {
            var set = false;

            MyMvc.StartsFrom<CustomStartup>()
                .WithApplication(app =>
                {
                    set = true;
                });

            var service = TestApplication.Services.GetRequiredService<IInjectedService>();

            Assert.NotNull(service);

            Assert.True(set);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomStartupAndAdditionalRoutesShouldWorkCorrectly()
        {
            MyMvc.StartsFrom<CustomStartup>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutAdditionalServicesTheDefaultActionInvokersShouldBeSet()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var services = TestApplication.Services;
            var actionInvokerProviders = services.GetServices<IActionInvokerProvider>();
            var modelBindingActionInvokerFactory = services.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(1, actionInvokerProviders.Count());
            Assert.True(actionInvokerProviders.Any(a => a.GetType() == typeof(ControllerActionInvokerProvider)));
            Assert.Null(modelBindingActionInvokerFactory);

            var routeServices = TestApplication.RouteServices;
            var routeActionInvokerProviders = routeServices.GetServices<IActionInvokerProvider>();
            var routeModelBindingActionInvokerFactory = routeServices.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(2, routeActionInvokerProviders.Count());

            var routeActionInvokerProvidersList = routeActionInvokerProviders.OrderByDescending(r => r.Order).ToList();

            Assert.True(routeActionInvokerProvidersList[0].GetType() == typeof(ModelBindingActionInvokerProvider));
            Assert.True(routeActionInvokerProvidersList[1].GetType() == typeof(ControllerActionInvokerProvider));
            Assert.NotNull(routeModelBindingActionInvokerFactory);
            Assert.IsAssignableFrom<ModelBindingActionInvokerFactory>(routeModelBindingActionInvokerFactory);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomImplementationsForTheRouteTestingTheCorrectServicesShouldBeSet()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(customServices =>
                {
                    customServices.TryAddEnumerable(
                        ServiceDescriptor.Transient<IActionInvokerProvider, CustomActionInvokerProvider>());
                    customServices.TryAddSingleton<IModelBindingActionInvokerFactory, CustomModelBindingActionInvokerFactory>();
                });

            var services = TestApplication.Services;
            var actionInvokerProviders = services.GetServices<IActionInvokerProvider>();
            var modelBindingActionInvokerFactory = services.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(1, actionInvokerProviders.Count());
            Assert.True(actionInvokerProviders.Any(a => a.GetType() == typeof(ControllerActionInvokerProvider)));
            Assert.Null(modelBindingActionInvokerFactory);

            var routeServices = TestApplication.RouteServices;
            var routeActionInvokerProviders = routeServices.GetServices<IActionInvokerProvider>();
            var routeModelBindingActionInvokerFactory = routeServices.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(2, routeActionInvokerProviders.Count());

            var routeActionInvokerProvidersList = routeActionInvokerProviders.OrderByDescending(r => r.Order).ToList();

            Assert.True(routeActionInvokerProvidersList[0].GetType() == typeof(CustomActionInvokerProvider));
            Assert.True(routeActionInvokerProvidersList[1].GetType() == typeof(ControllerActionInvokerProvider));
            Assert.NotNull(routeModelBindingActionInvokerFactory);
            Assert.IsAssignableFrom<CustomModelBindingActionInvokerFactory>(routeModelBindingActionInvokerFactory);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutHttpContextFactoryTheDefaultMockedHttpContextShouldBeProvided()
        {
            MyMvc.IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryRemoveTransient<IHttpContextFactory>();
                });

            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();

            Assert.Null(httpContextFactory);

            var httpContext = TestHelper.CreateMockedHttpContext();

            Assert.NotNull(httpContext);
            Assert.Equal(ContentType.FormUrlEncoded, httpContext.Request.ContentType);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextFactoryShouldReturnMockedHttpContextBasedOnTheFactoryCreatedHttpContext()
        {
            MyMvc.IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                });

            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();

            Assert.NotNull(httpContextFactory);

            var httpContext = TestHelper.CreateMockedHttpContext();

            Assert.NotNull(httpContext);
            Assert.Equal(ContentType.AudioVorbis, httpContext.Request.ContentType);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IHttpContextAccessorShouldWorkCorrectlySynchronously()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });
            
            HttpContext firstContext = null;
            HttpContext secondContext = null;

            firstContext = MyMvc
                            .Controller<HttpContextController>()
                            .AndProvideTheController().Context;

            secondContext = MyMvc
                            .Controller<HttpContextController>()
                            .AndProvideTheController().Context;

            Assert.NotNull(firstContext);
            Assert.NotNull(secondContext);
            Assert.IsAssignableFrom<MockedHttpContext>(firstContext);
            Assert.IsAssignableFrom<MockedHttpContext>(secondContext);
            Assert.NotSame(firstContext, secondContext);
            Assert.Equal(ContentType.AudioVorbis, firstContext.Request.ContentType);
            Assert.Equal(ContentType.AudioVorbis, secondContext.Request.ContentType);
            
            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IHttpContextAccessorShouldWorkCorrectlyAsynchronously()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            Task
                .Run(async () =>
                {
                    HttpContext firstContextAsync = null;
                    HttpContext secondContextAsync = null;
                    HttpContext thirdContextAsync = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            firstContextAsync = MyMvc
                                .Controller<HttpContextController>()
                                .AndProvideTheController().Context;
                        }),
                        Task.Run(() =>
                        {
                            secondContextAsync = MyMvc
                                .Controller<HttpContextController>()
                                .AndProvideTheController().Context;
                        }),
                        Task.Run(() =>
                        {
                            thirdContextAsync = MyMvc
                                .Controller<HttpContextController>()
                                .AndProvideTheController().Context;
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.NotNull(firstContextAsync);
                    Assert.NotNull(secondContextAsync);
                    Assert.NotNull(thirdContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(firstContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(secondContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(thirdContextAsync);
                    Assert.NotSame(firstContextAsync, secondContextAsync);
                    Assert.NotSame(firstContextAsync, thirdContextAsync);
                    Assert.NotSame(secondContextAsync, thirdContextAsync);
                    Assert.Equal(ContentType.AudioVorbis, firstContextAsync.Request.ContentType);
                    Assert.Equal(ContentType.AudioVorbis, secondContextAsync.Request.ContentType);
                    Assert.Equal(ContentType.AudioVorbis, thirdContextAsync.Request.ContentType);
                })
                .GetAwaiter()
                .GetResult();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MockedMemoryCacheShouldBeRegistedByDefault()
        {
            MyMvc.IsUsingDefaultConfiguration();

            Assert.IsAssignableFrom<MockedMemoryCache>(TestServiceProvider.GetService<IMemoryCache>());
        }

        [Fact]
        public void MockedMemoryCacheShouldBeRegistedWithAddedCaching()
        {
            MyMvc.IsUsingDefaultConfiguration()
                .WithServices(services => services.AddCaching());

            Assert.IsAssignableFrom<MockedMemoryCache>(TestServiceProvider.GetService<IMemoryCache>());
        }

        [Fact]
        public void MockedMemoryCacheShouldBeDifferentForEveryCallSynchronously()
        {
            // second call should not have cache entries
            MyMvc
                .Controller<MvcController>()
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .Ok();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void MockedMemoryCacheShouldBeDifferentForEveryCallSynchronouslyWithCachedControllerBuilder()
        {
            var controller = MyMvc.Controller<MvcController>();

            // second call should not have cache entries
            controller
                .WithMemoryCache(cache => cache.WithEntry("test", "value"))
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .Ok();

            controller
                .Calling(c => c.MemoryCacheAction())
                .ShouldReturn()
                .BadRequest();
        }
        
        [Fact]
        public void MockedMemoryCacheShouldBeDifferentForEveryCallAsynchronously()
        {
            Task
                .Run(async () =>
                {
                    TestHelper.ClearMemoryCache();

                    string firstValue = null;
                    string secondValue = null;
                    string thirdValue = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "first");
                            firstValue = memoryCache.Get<string>("test");
                            TestHelper.ClearMemoryCache();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "second");
                            secondValue = memoryCache.Get<string>("test");
                            TestHelper.ClearMemoryCache();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "third");
                            thirdValue = memoryCache.Get<string>("test");
                            TestHelper.ClearMemoryCache();
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.Equal("first", firstValue);
                    Assert.Equal("second", secondValue);
                    Assert.Equal("third", thirdValue);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
