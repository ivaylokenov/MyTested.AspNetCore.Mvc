using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MyTested.Mvc.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Internal;
    using Internal.Application;
    using Internal.Caching;
    using Internal.Contracts;
    using Internal.Controllers;
    using Internal.Formatters;
    using Internal.Http;
    using Internal.Routes;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Services;
    using Setups.Startups;

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
                "A public method named 'ConfigureTest' or 'Configure' could not be found in the 'MyTested.Mvc.Test.Setups.Controllers.MvcController' type.");
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
            MyMvc
                .Controller<MvcController>()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.IsAssignableFrom<MvcController>(controller);
                });
        }

        [Fact]
        public void ControllerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            MyMvc
                .Controller(() => new MvcController(new InjectedService()))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.IsAssignableFrom<MvcController>(controller);

                    Assert.NotNull(controller.InjectedService);
                    Assert.IsAssignableFrom<InjectedService>(controller.InjectedService);
                });
        }

        [Fact]
        public void ControllerWithProvidedInstanceShouldPopulateCorrectInstanceOfControllerType()
        {
            var instance = new MvcController();

            MyMvc
                .Controller(instance)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.IsAssignableFrom<MvcController>(controller);
                });
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndWithRegisteredServicesShouldPopulateCorrectInstanceOfControllerType()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddTransient<IInjectedService, InjectedService>();
                    services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                });

            MyMvc
                .Controller<NoParameterlessConstructorController>()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Service);
                    Assert.IsAssignableFrom<InjectedService>(controller.Service);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.IsAssignableFrom<AnotherInjectedService>(controller.AnotherInjectedService);
                });
            
            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndNoServicesShouldThrowProperException()
        {
            MyMvc.IsUsingDefaultConfiguration();

            Test.AssertException<UnresolvedServicesException>(
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
        public void CustomConfigureOptionsShouldNotOverrideTheDefaultTestOnes()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.Configure<MvcOptions>(options =>
                    {
                        options.MaxModelValidationErrors = 120;
                    });
                });

            var builtOptions = TestApplication.Services.GetRequiredService<IOptions<MvcOptions>>();

            Assert.Equal(120, builtOptions.Value.MaxModelValidationErrors);
            Assert.Contains(typeof(StringInputFormatter), builtOptions.Value.InputFormatters.Select(f => f.GetType()));
            Assert.Equal(1, builtOptions.Value.Conventions.Count);

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
                    services.AddHttpContextAccessor();
                });

            HttpContext firstContext = null;
            HttpContext secondContext = null;

            MyMvc
                .Controller<HttpContextController>()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    firstContext = controller.Context;
                });

            MyMvc
                .Controller<HttpContextController>()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    secondContext = controller.Context;
                });

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
                    services.AddHttpContextAccessor();
                });

            Task
                .Run(async () =>
                {
                    HttpContext firstContextAsync = null;
                    HttpContext secondContextAsync = null;
                    HttpContext thirdContextAsync = null;
                    HttpContext fourthContextAsync = null;
                    HttpContext fifthContextAsync = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<HttpContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    firstContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<HttpContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    secondContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<HttpContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    thirdContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<HttpContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fourthContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<HttpContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fifthContextAsync = controller.Context;
                                });
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.NotNull(firstContextAsync);
                    Assert.NotNull(secondContextAsync);
                    Assert.NotNull(thirdContextAsync);
                    Assert.NotNull(fourthContextAsync);
                    Assert.NotNull(fifthContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(firstContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(secondContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(thirdContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(fourthContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(fifthContextAsync);
                    Assert.NotSame(firstContextAsync, secondContextAsync);
                    Assert.NotSame(firstContextAsync, thirdContextAsync);
                    Assert.NotSame(secondContextAsync, thirdContextAsync);
                    Assert.NotSame(thirdContextAsync, fourthContextAsync);
                    Assert.NotSame(fourthContextAsync, fifthContextAsync);
                    Assert.NotSame(thirdContextAsync, fifthContextAsync);
                })
                .GetAwaiter()
                .GetResult();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomHttpContextShouldSetItToAccessor()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = ContentType.AudioVorbis;

            MyMvc
                .Controller<HttpContextController>()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.HttpContext);
                    Assert.NotNull(controller.Context);
                    Assert.Equal(ContentType.AudioVorbis, controller.HttpContext.Request.ContentType);
                    Assert.Equal(ContentType.AudioVorbis, controller.Context.Request.ContentType);
                });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IActionContextAccessorShouldWorkCorrectlySynchronously()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            ActionContext firstContext = null;
            ActionContext secondContext = null;

            MyMvc
                .Controller<ActionContextController>()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    firstContext = controller.Context;
                });

            MyMvc
                .Controller<ActionContextController>()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    secondContext = controller.Context;
                });

            Assert.NotNull(firstContext);
            Assert.NotNull(secondContext);
            Assert.IsAssignableFrom<MockedControllerContext>(firstContext);
            Assert.IsAssignableFrom<MockedControllerContext>(secondContext);
            Assert.NotSame(firstContext, secondContext);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IActionContextAccessorShouldWorkCorrectlyAsynchronously()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            Task
                .Run(async () =>
                {
                    ActionContext firstContextAsync = null;
                    ActionContext secondContextAsync = null;
                    ActionContext thirdContextAsync = null;
                    ActionContext fourthContextAsync = null;
                    ActionContext fifthContextAsync = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<ActionContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    firstContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<ActionContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    secondContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<ActionContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    thirdContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<ActionContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fourthContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyMvc
                                .Controller<ActionContextController>()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fifthContextAsync = controller.Context;
                                });
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.NotNull(firstContextAsync);
                    Assert.NotNull(secondContextAsync);
                    Assert.NotNull(thirdContextAsync);
                    Assert.NotNull(fourthContextAsync);
                    Assert.NotNull(fifthContextAsync);
                    Assert.IsAssignableFrom<MockedControllerContext>(firstContextAsync);
                    Assert.IsAssignableFrom<MockedControllerContext>(secondContextAsync);
                    Assert.IsAssignableFrom<MockedControllerContext>(thirdContextAsync);
                    Assert.IsAssignableFrom<MockedControllerContext>(fourthContextAsync);
                    Assert.IsAssignableFrom<MockedControllerContext>(fifthContextAsync);
                    Assert.NotSame(firstContextAsync, secondContextAsync);
                    Assert.NotSame(firstContextAsync, thirdContextAsync);
                    Assert.NotSame(secondContextAsync, thirdContextAsync);
                    Assert.NotSame(thirdContextAsync, fourthContextAsync);
                    Assert.NotSame(fourthContextAsync, fifthContextAsync);
                    Assert.NotSame(thirdContextAsync, fifthContextAsync);
                })
                .GetAwaiter()
                .GetResult();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomActionContextShouldSetItToAccessor()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            var actionDescriptor = new ControllerActionDescriptor { Name = "Test" };
            var actionContext = new ActionContext { ActionDescriptor = actionDescriptor };

            MyMvc
                .Controller<ActionContextController>()
                .WithActionContext(actionContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.Name);
                });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomActionContextFuncShouldSetItToAccessor()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            var actionDescriptor = new ControllerActionDescriptor { Name = "Test" };

            MyMvc
                .Controller<ActionContextController>()
                .WithActionContext(actionContext =>
                {
                    actionContext.ActionDescriptor = actionDescriptor;
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.Name);
                });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomControllerContextShouldSetItToAccessor()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            var actionDescriptor = new ControllerActionDescriptor { Name = "Test" };
            var actionContext = new ControllerContext { ActionDescriptor = actionDescriptor };

            MyMvc
                .Controller<ActionContextController>()
                .WithControllerContext(actionContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.Name);
                });

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
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());

            Assert.IsAssignableFrom<MockedMemoryCache>(TestServiceProvider.GetService<IMemoryCache>());

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void MockedMemoryCacheShouldNotBeRegisteredIfNoCacheIsAdded()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.TryRemoveSingleton<IMemoryCache>());

            Assert.Null(TestServiceProvider.GetService<IMemoryCache>());

            MyMvc.IsUsingDefaultConfiguration();
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
                    TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                    TestHelper.ExecuteTestCleanup();

                    string firstValue = null;
                    string secondValue = null;
                    string thirdValue = null;
                    string fourthValue = null;
                    string fifthValue = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "first");
                            firstValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "second");
                            secondValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "third");
                            thirdValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "fourth");
                            fourthValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        }),
                        Task.Run(() =>
                        {
                            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
                            memoryCache.Set("test", "fifth");
                            fifthValue = TestServiceProvider.GetService<IMemoryCache>().Get<string>("test");
                            TestHelper.ExecuteTestCleanup();
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.Equal("first", firstValue);
                    Assert.Equal("second", secondValue);
                    Assert.Equal("third", thirdValue);
                    Assert.Equal("fourth", fourthValue);
                    Assert.Equal("fifth", fifthValue);
                })
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public void DefaultConfigurationShouldSetMockedMemoryCache()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MockedMemoryCache>(memoryCache);
        }

        [Fact]
        public void CustomMemoryCacheShouldOverrideTheMockedOne()
        {
            MyMvc.StartsFrom<DataStartup>();

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<CustomMemoryCache>(memoryCache);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ExplicitMockedMemoryCacheShouldOverrideIt()
        {
            MyMvc
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceMemoryCache();
                });

            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();

            Assert.NotNull(memoryCache);
            Assert.IsAssignableFrom<MockedMemoryCache>(memoryCache);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigurationShouldSetMockedSession()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.Null(session);
        }

        [Fact]
        public void DefaultConfigurationWithSessionShouldSetMockedSession()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<MockedSessionStore>(session);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CustomSessionShouldOverrideTheMockedOne()
        {
            MyMvc.StartsFrom<DataStartup>();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<CustomSessionStore>(session);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ExplicitMockedSessionShouldOverrideIt()
        {
            MyMvc
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<MockedSessionStore>(session);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigurationShouldSetMockedTempDataProvider()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<MockedTempDataProvider>(tempDataProvider);
        }

        [Fact]
        public void CustomTempDataProviderShouldOverrideTheMockedOne()
        {
            MyMvc.StartsFrom<DataStartup>();

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<CustomTempDataProvider>(tempDataProvider);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ExplicitMockedTempDataProviderShouldOverrideIt()
        {
            MyMvc
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceTempDataProvider();
                });

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<MockedTempDataProvider>(tempDataProvider);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ScopedServicesShouldRemainThroughTheTestCase()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddScoped<IScopedService, ScopedService>();
                });

            MyMvc
                .Controller<ServicesController>()
                .Calling(c => c.SetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Scoped");
            
            MyMvc
                .Controller<ServicesController>()
                .WithNoServiceFor<IScopedService>()
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Default");

            MyMvc
                .Controller<ServicesController>()
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Constructor");

            MyMvc
                .Controller<ServicesController>()
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Constructor");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ServiceLifeTimesShouldBeSavedCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
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
            
            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
