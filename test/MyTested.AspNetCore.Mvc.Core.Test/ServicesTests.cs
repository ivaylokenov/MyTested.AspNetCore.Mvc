namespace MyTested.AspNetCore.Mvc.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Internal;
    using Internal.Application;
    using Internal.Contracts;
    using Internal.Controllers;
    using Internal.Formatters;
    using Internal.Http;
    using Internal.Routing;
    using Internal.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
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
            MyApplication.IsUsingDefaultConfiguration();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);
        }

        [Fact]
        public void IsUsingShouldAddServices()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingShouldAddServicesWithOptions()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var initialSetOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            MyApplication.IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesWithOptionsRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            var setOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            Assert.NotNull(setOptions);
            Assert.Equal(initialSetOptions.Count() + 1, setOptions.Count());

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithAction()
        {
            MyApplication.StartsFrom<CustomStartup>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithFunc()
        {
            MyApplication.StartsFrom<CustomStartupWithBuiltProvider>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyApplication.IsUsingDefaultConfiguration();
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
            Assert.IsAssignableFrom(typeof(InjectedService), injectedServices);

            MyApplication.IsUsingDefaultConfiguration();
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
            MyApplication.IsUsingDefaultConfiguration();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);

            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyApplication.IsUsingDefaultConfiguration();

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    injectedService = TestServiceProvider.GetRequiredService<IInjectedService>();
                },
                "IInjectedService could not be resolved from the services provider. Before running this test case, the service should be registered in the 'StartsFrom' method and cannot be null.");

            MyApplication.IsUsingDefaultConfiguration()
                .WithServices(TestObjectFactory.GetCustomServicesRegistrationAction());

            injectedService = TestServiceProvider.GetService<IInjectedService>();
            anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ControllerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            MyApplication.IsUsingDefaultConfiguration();

            MyController<MvcController>
                .Instance()
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
            MyController<MvcController>
                .Instance(() => new MvcController(new InjectedService()))
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

            MyController<MvcController>
                .Instance(instance)
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
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddTransient<IInjectedService, InjectedService>();
                    services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                });

            MyController<NoParameterlessConstructorController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Service);
                    Assert.IsAssignableFrom<InjectedService>(controller.Service);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.IsAssignableFrom<AnotherInjectedService>(controller.AnotherInjectedService);
                });
            
            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndNoServicesShouldThrowProperException()
        {
            MyApplication.IsUsingDefaultConfiguration();

            Test.AssertException<UnresolvedServicesException>(
                () =>
                {
                    MyController<NoParameterlessConstructorController>
                        .Instance()
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking no parameters.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigShouldSetMvc()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var service = TestServiceProvider.GetRequiredService<MvcMarkerService>();

            Assert.NotNull(service);
        }

        [Fact]
        public void DefaultConfigShouldSetDefaultRoutes()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);
        }

        [Fact]
        public void DefaultConfigAndAdditionalServicesShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMvc();
                });

            var service = TestServiceProvider.GetRequiredService<MvcMarkerService>();

            Assert.NotNull(service);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigAndAdditionalApplicationShouldWorkCorrectly()
        {
            var set = false;

            MyApplication
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
            MyApplication
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
            MyApplication.StartsFrom<CustomStartup>();

            var service = TestApplication.Services.GetRequiredService<IInjectedService>();

            Assert.NotNull(service);

            var routes = TestApplication.Router as RouteCollection;

            Assert.NotNull(routes);
            Assert.Equal(2, routes.Count);

            MyApplication.IsUsingDefaultConfiguration();
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CustomStartupWithAdditionalApplicationShouldWorkCorrectly()
        {
            var set = false;

            MyApplication.StartsFrom<CustomStartup>()
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

            MyApplication.IsUsingDefaultConfiguration();
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutAdditionalServicesTheDefaultActionInvokersShouldBeSet()
        {
            MyApplication.IsUsingDefaultConfiguration();

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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomImplementationsForTheRouteTestingTheCorrectServicesShouldBeSet()
        {
            MyApplication
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

            Assert.Equal(2, actionInvokerProviders.Count());
            Assert.True(actionInvokerProviders.Any(a => a.GetType() == typeof(ControllerActionInvokerProvider)));
            Assert.True(actionInvokerProviders.Any(a => a.GetType() == typeof(CustomActionInvokerProvider)));
            Assert.NotNull(modelBindingActionInvokerFactory);

            var routeServices = TestApplication.RouteServices;
            var routeActionInvokerProviders = routeServices.GetServices<IActionInvokerProvider>();
            var routeModelBindingActionInvokerFactory = routeServices.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(2, routeActionInvokerProviders.Count());

            var routeActionInvokerProvidersList = routeActionInvokerProviders.OrderByDescending(r => r.Order).ToList();

            Assert.True(routeActionInvokerProvidersList[0].GetType() == typeof(CustomActionInvokerProvider));
            Assert.True(routeActionInvokerProvidersList[1].GetType() == typeof(ControllerActionInvokerProvider));
            Assert.NotNull(routeModelBindingActionInvokerFactory);
            Assert.IsAssignableFrom<CustomModelBindingActionInvokerFactory>(routeModelBindingActionInvokerFactory);

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void CustomConfigureOptionsShouldNotOverrideTheDefaultTestOnes()
        {
            MyApplication
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutHttpContextFactoryTheDefaultMockedHttpContextShouldBeProvided()
        {
            MyApplication.IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.RemoveTransient<IHttpContextFactory>();
                });

            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();

            Assert.Null(httpContextFactory);

            var httpContext = TestHelper.CreateMockedHttpContext();

            Assert.NotNull(httpContext);
            Assert.Equal(ContentType.FormUrlEncoded, httpContext.Request.ContentType);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextFactoryShouldReturnMockedHttpContextBasedOnTheFactoryCreatedHttpContext()
        {
            MyApplication.IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.ReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                });

            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();

            Assert.NotNull(httpContextFactory);

            var httpContext = TestHelper.CreateMockedHttpContext();

            Assert.NotNull(httpContext);
            Assert.Equal(ContentType.AudioVorbis, httpContext.Request.ContentType);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IHttpContextAccessorShouldWorkCorrectlySynchronously()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.ReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                    services.AddHttpContextAccessor();
                });

            HttpContext firstContext = null;
            HttpContext secondContext = null;

            MyController<HttpContextController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    firstContext = controller.Context;
                });

            MyController<HttpContextController>
                .Instance()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IHttpContextAccessorShouldWorkCorrectlyAsynchronously()
        {
            MyApplication
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
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    firstContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    secondContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    thirdContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fourthContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
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
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomHttpContextShouldSetItToAccessor()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = ContentType.AudioVorbis;

            MyController<HttpContextController>
                .Instance()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IActionContextAccessorShouldWorkCorrectlySynchronously()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            ActionContext firstContext = null;
            ActionContext secondContext = null;

            MyController<ActionContextController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    firstContext = controller.Context;
                });

            MyController<ActionContextController>
                .Instance()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IActionContextAccessorShouldWorkCorrectlyAsynchronously()
        {
            MyApplication
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
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    firstContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    secondContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    thirdContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fourthContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
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
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomActionContextShouldSetItToAccessor()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            var actionDescriptor = new ControllerActionDescriptor { DisplayName = "Test" };
            var actionContext = new ActionContext { ActionDescriptor = actionDescriptor };

            MyController<ActionContextController>
                .Instance()
                .WithActionContext(actionContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.DisplayName);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomActionContextFuncShouldSetItToAccessor()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            var actionDescriptor = new ControllerActionDescriptor { DisplayName = "Test" };

            MyController<ActionContextController>
                .Instance()
                .WithActionContext(actionContext =>
                {
                    actionContext.ActionDescriptor = actionDescriptor;
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.DisplayName);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomControllerContextShouldSetItToAccessor()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });

            var actionDescriptor = new ControllerActionDescriptor { DisplayName = "Test" };
            var actionContext = new ControllerContext { ActionDescriptor = actionDescriptor };

            MyController<ActionContextController>
                .Instance()
                .WithControllerContext(actionContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.DisplayName);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void MockedMemoryCacheShouldNotBeRegisteredIfNoCacheIsAdded()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.RemoveSingleton<IMemoryCache>());

            Assert.Null(TestServiceProvider.GetService<IMemoryCache>());

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void CustomTempDataProviderShouldOverrideTheMockedOne()
        {
            MyApplication.StartsFrom<DataStartup>();

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<CustomTempDataProvider>(tempDataProvider);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ScopedServicesShouldRemainThroughTheTestCase()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddScoped<IScopedService, ScopedService>();
                });

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.SetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Scoped");

            MyController<ServicesController>
                .Instance()
                .WithNoServiceFor<IScopedService>()
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Default");

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Constructor");

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Constructor");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ServiceLifeTimesShouldBeSavedCorrectly()
        {
            MyApplication
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
            
            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
