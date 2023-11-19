#pragma warning disable xUnit1030
#pragma warning disable xUnit1031
namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exceptions;
    using Internal.Contracts;
    using Internal.ViewComponents;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Services;
    using Setups.ViewComponents;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void ViewComponentBuilderWithEmptyConstructorShouldNotBeNull()
        {
            var viewComponentBuilder = new MyViewComponent<NormalComponent>();

            Assert.NotNull(viewComponentBuilder);
        }

        [Fact]
        public void ViewComponentBuilderWithConstructorWithViewComponentShouldNotBeNull()
        {
            var viewComponent = new NormalComponent();
            var viewComponentBuilder = new MyViewComponent<NormalComponent>(viewComponent);

            Assert.NotNull(viewComponentBuilder);
            Assert.IsAssignableFrom<NormalComponent>(viewComponentBuilder.TestContext.Component);
        }

        [Fact]
        public void ViewComponentWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfViewComponentType()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            MyViewComponent<NormalComponent>
                .Instance()
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.IsAssignableFrom<NormalComponent>(viewComponent);
                });
        }

        [Fact]
        public void ViewComponentWithConstructorFunctionShouldPopulateCorrectNewInstanceOfViewComponentType()
        {
            MyViewComponent<ServicesComponent>
                .Instance(() => new ServicesComponent(new InjectedService()))
                .ShouldPassForThe<ServicesComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.IsAssignableFrom<ServicesComponent>(viewComponent);

                    Assert.NotNull(viewComponent.Service);
                    Assert.IsAssignableFrom<InjectedService>(viewComponent.Service);
                });
        }

        [Fact]
        public void ViewComponentWithProvidedInstanceShouldPopulateCorrectInstanceOfViewComponentType()
        {
            var instance = new NormalComponent();

            MyViewComponent<NormalComponent>
                .Instance(instance)
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.IsAssignableFrom<NormalComponent>(viewComponent);
                });
        }

        [Fact]
        public void ViewComponentWithNoParameterlessConstructorAndWithRegisteredServicesShouldPopulateCorrectInstanceOfViewComponentType()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddTransient<IInjectedService, InjectedService>();
                });

            MyViewComponent<ServicesComponent>
                .Instance()
                .ShouldPassForThe<ServicesComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.Service);
                    Assert.IsAssignableFrom<InjectedService>(viewComponent.Service);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ViewComponentWithNoParameterlessConstructorAndNoServicesShouldThrowProperException()
        {
            Test.AssertException<UnresolvedServicesException>(
                () =>
                {
                    MyViewComponent<ServicesComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .View();
                },
                "ServicesComponent could not be instantiated because it contains no constructor taking no parameters.");
        }

        [Fact]
        public void IActionContextAccessorShouldWorkCorrectlySynchronously()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            ActionContext firstContext = null;
            ActionContext secondContext = null;

            MyViewComponent<AccessorComponent>
                .Instance()
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    firstContext = viewComponent.ActionContext;
                });

            MyViewComponent<AccessorComponent>
                .Instance()
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    secondContext = viewComponent.ActionContext;
                });

            Assert.NotNull(firstContext);
            Assert.NotNull(secondContext);
            Assert.IsAssignableFrom<ViewContextMock>(firstContext);
            Assert.IsAssignableFrom<ViewContextMock>(secondContext);
            Assert.NotSame(firstContext, secondContext);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void IActionContextAccessorShouldWorkCorrectlyAsynchronously()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
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
                            MyViewComponent<AccessorComponent>
                                .Instance()
                                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                                {
                                    firstContextAsync = viewComponent.ActionContext;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyViewComponent<AccessorComponent>
                                .Instance()
                                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                                {
                                    secondContextAsync = viewComponent.ActionContext;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyViewComponent<AccessorComponent>
                                .Instance()
                                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                                {
                                    thirdContextAsync = viewComponent.ActionContext;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyViewComponent<AccessorComponent>
                                .Instance()
                                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                                {
                                    fourthContextAsync = viewComponent.ActionContext;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyViewComponent<AccessorComponent>
                                .Instance()
                                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                                {
                                    fifthContextAsync = viewComponent.ActionContext;
                                });
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.NotNull(firstContextAsync);
                    Assert.NotNull(secondContextAsync);
                    Assert.NotNull(thirdContextAsync);
                    Assert.NotNull(fourthContextAsync);
                    Assert.NotNull(fifthContextAsync);
                    Assert.IsAssignableFrom<ViewContextMock>(firstContextAsync);
                    Assert.IsAssignableFrom<ViewContextMock>(secondContextAsync);
                    Assert.IsAssignableFrom<ViewContextMock>(thirdContextAsync);
                    Assert.IsAssignableFrom<ViewContextMock>(fourthContextAsync);
                    Assert.IsAssignableFrom<ViewContextMock>(fifthContextAsync);
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

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithCustomActionContextShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var actionDescriptor = new ActionDescriptor { DisplayName = "Test" };
            var actionContext = new ActionContext { ActionDescriptor = actionDescriptor };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithActionContext(actionContext)
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ActionContext);
                    Assert.Equal("Test", viewComponent.ActionContext.ActionDescriptor.DisplayName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithCustomActionContextFuncShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var actionDescriptor = new ActionDescriptor { DisplayName = "Test" };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithActionContext(actionContext =>
                {
                    actionContext.ActionDescriptor = actionDescriptor;
                })
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ActionContext);
                    Assert.Equal("Test", viewComponent.ActionContext.ActionDescriptor.DisplayName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithCustomViewComponentContextShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var actionDescriptor = new ActionDescriptor { DisplayName = "Test" };
            var context = new ViewComponentContext { ViewContext = new ViewContext { ActionDescriptor = actionDescriptor } };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewComponentContext(context)
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ActionContext);
                    Assert.Equal("Test", viewComponent.ActionContext.ActionDescriptor.DisplayName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewContextWithoutViewDataShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            ViewDataDictionary viewData = null;

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewContext(viewContext =>
                {
                    viewContext.ViewData = null;
                })
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    viewData = viewComponent.ViewContext.ViewData;
                });

            Assert.NotNull(viewData);
            Assert.IsAssignableFrom<ViewDataDictionaryMock>(viewData);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewComponentContextWithoutViewComponentDescriptorShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var context = new ViewComponentContext { ViewComponentDescriptor = null };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewComponentContext(context)
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ViewComponentContext);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewComponentDescriptor);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewComponentContextWithoutViewContextShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var context = new ViewComponentContext { ViewContext = null };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewComponentContext(context)
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ViewContext);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewComponentContextFuncShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var actionDescriptor = new ActionDescriptor { DisplayName = "Test" };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewComponentContext(context =>
                {
                    context.ViewContext.ActionDescriptor = actionDescriptor;
                })
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ActionContext);
                    Assert.Equal("Test", viewComponent.ActionContext.ActionDescriptor.DisplayName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithCustomViewContextShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var actionDescriptor = new ActionDescriptor { DisplayName = "Test" };
            var context = new ViewContext { ActionDescriptor = actionDescriptor };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewContext(context)
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ActionContext);
                    Assert.Equal("Test", viewComponent.ActionContext.ActionDescriptor.DisplayName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewContextWithoutValidationMessageElementShouldSetItToAccessor()
        {
            var mvcOptions = new MvcViewOptions()
            {
                HtmlHelperOptions = new HtmlHelperOptions()
            };

            mvcOptions.HtmlHelperOptions.ValidationMessageElement = "Test";

            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton(mvcOptions);
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewContext(viewContext =>
                {
                    viewContext.ValidationMessageElement = null;
                })
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent.ViewContext.ValidationMessageElement);
                    Assert.True(viewComponent.ViewContext.ValidationMessageElement == "Test");
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewContextWithoutValidationSummaryMessageElementShouldSetItToAccessor()
        {
            var mvcOptions = new MvcViewOptions()
            {
                HtmlHelperOptions = new HtmlHelperOptions()
            };

            mvcOptions.HtmlHelperOptions.ValidationSummaryMessageElement = "Test";

            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton(mvcOptions);
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewContext(viewContext =>
                {
                    viewContext.ValidationSummaryMessageElement = null;
                })
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent.ViewContext.ValidationSummaryMessageElement);
                    Assert.True(viewComponent.ViewContext.ValidationSummaryMessageElement == "Test");
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithViewContextFuncShouldSetItToAccessor()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                });

            var actionDescriptor = new ActionDescriptor { DisplayName = "Test" };

            MyViewComponent<AccessorComponent>
                .Instance()
                .WithViewContext(context =>
                {
                    context.ActionDescriptor = actionDescriptor;
                })
                .ShouldPassForThe<AccessorComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ActionContext);
                    Assert.Equal("Test", viewComponent.ActionContext.ActionDescriptor.DisplayName);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ViewComponentDescriptorCacheShouldWorkCorrectlyAndReturnDescriptor()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldPassForThe<NormalComponent>(vc =>
                {
                    var cache = vc.HttpContext.RequestServices.GetService<IViewComponentDescriptorCache>();
                    var viewComponentDescriptor = cache.GetViewComponentDescriptor(vc.GetType().GetMethod("Invoke"));

                    Assert.True(viewComponentDescriptor.GetType() == typeof(ViewComponentDescriptor));
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
