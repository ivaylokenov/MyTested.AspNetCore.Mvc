namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exceptions;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Setups.Services;
    using Xunit;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class ServicesTests
    {
        [Fact]
        public void ControllerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            MyApplication.IsUsingDefaultConfiguration();

            MyController<MvcController>
                .Instance()
                .ShouldPassForThe<MvcController>(controller =>
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
                .ShouldPassForThe<MvcController>(controller =>
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
                .ShouldPassForThe<MvcController>(controller =>
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
                .ShouldPassForThe<NoParameterlessConstructorController>(controller =>
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
                .ShouldPassForThe<ActionContextController>(controller =>
                {
                    firstContext = controller.Context;
                });

            MyController<ActionContextController>
                .Instance()
                .ShouldPassForThe<ActionContextController>(controller =>
                {
                    secondContext = controller.Context;
                });

            Assert.NotNull(firstContext);
            Assert.NotNull(secondContext);
            Assert.IsAssignableFrom<ControllerContextMock>(firstContext);
            Assert.IsAssignableFrom<ControllerContextMock>(secondContext);
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
                                .ShouldPassForThe<ActionContextController>(controller =>
                                {
                                    firstContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassForThe<ActionContextController>(controller =>
                                {
                                    secondContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassForThe<ActionContextController>(controller =>
                                {
                                    thirdContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassForThe<ActionContextController>(controller =>
                                {
                                    fourthContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<ActionContextController>
                                .Instance()
                                .ShouldPassForThe<ActionContextController>(controller =>
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
                    Assert.IsAssignableFrom<ControllerContextMock>(firstContextAsync);
                    Assert.IsAssignableFrom<ControllerContextMock>(secondContextAsync);
                    Assert.IsAssignableFrom<ControllerContextMock>(thirdContextAsync);
                    Assert.IsAssignableFrom<ControllerContextMock>(fourthContextAsync);
                    Assert.IsAssignableFrom<ControllerContextMock>(fifthContextAsync);
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
                .ShouldPassForThe<ActionContextController>(controller =>
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
                .ShouldPassForThe<ActionContextController>(controller =>
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
                .ShouldPassForThe<ActionContextController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.Context);
                    Assert.Equal("Test", controller.Context.ActionDescriptor.DisplayName);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
