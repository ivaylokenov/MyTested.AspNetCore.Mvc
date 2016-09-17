namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Xunit;
    using Exceptions;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValues()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IInjectedService>()
                    .WithNo<RequestModel>()
                    .WithNo<IAnotherInjectedService>())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructor()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IInjectedService>())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowException()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IInjectedService>())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValuesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IInjectedService>()
                    .WithNo<RequestModel>()
                    .WithNo<IAnotherInjectedService>())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructorForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IInjectedService>())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowExceptionForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IInjectedService>())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependencies()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .With<IInjectedService>(new InjectedService()))
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.Null(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependencies()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .With<IAnotherInjectedService>(new AnotherInjectedService())
                    .With<IInjectedService>(new InjectedService()))
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependencies()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .With<IAnotherInjectedService>(new AnotherInjectedService())
                    .With<RequestModel>(new RequestModel())
                    .With<IInjectedService>(new InjectedService()))
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.NotNull(controller.InjectedRequestModel);
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .With(new RequestModel())
                    .With(new AnotherInjectedService())
                    .With(new InjectedService()))
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(services => services
                    .With(new RequestModel())
                    .AndAlso()
                    .With(new AnotherInjectedService())
                    .AndAlso()
                    .With(new InjectedService()))
                .AndAlso()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();
        }


        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependenciesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .With<IInjectedService>(new InjectedService()))
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.Null(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependenciesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .With<IAnotherInjectedService>(new AnotherInjectedService())
                    .With<IInjectedService>(new InjectedService()))
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependenciesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .With<IAnotherInjectedService>(new AnotherInjectedService())
                    .With<RequestModel>(new RequestModel())
                    .With<IInjectedService>(new InjectedService()))
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.NotNull(controller.InjectedRequestModel);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuildersForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .With(new RequestModel())
                    .With(new AnotherInjectedService())
                    .With(new InjectedService()))
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuildersForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(services => services
                    .With(new RequestModel())
                    .AndAlso()
                    .With(new AnotherInjectedService())
                    .AndAlso()
                    .With(new InjectedService()))
                .AndAlso()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithCollectionOfObjectsForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjectsForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(new RequestModel(), new AnotherInjectedService(), new InjectedService())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenSameDependenciesAreRegisteredForPocoController()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<FullPocoController>
                        .Instance()
                        .WithServices(services => services
                            .With(new RequestModel())
                            .With<IAnotherInjectedService>(new AnotherInjectedService())
                            .With<IInjectedService>(new InjectedService())
                            .With<IAnotherInjectedService>(new AnotherInjectedService()));
                },
                "Dependency AnotherInjectedService is already registered.");
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithCollectionOfObjects()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjects()
        {
            MyController<MvcController>
                .Instance()
                .WithServices(new RequestModel(), new AnotherInjectedService(), new InjectedService())
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenSameDependenciesAreRegistered()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithServices(services => services
                            .With(new RequestModel())
                            .With<IAnotherInjectedService>(new AnotherInjectedService())
                            .With<IInjectedService>(new InjectedService())
                            .With<IAnotherInjectedService>(new AnotherInjectedService()));
                },
                "Dependency AnotherInjectedService is already registered.");
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependencies()
        {
            Test.AssertException<UnresolvedServicesException>(
                () =>
                {
                    MyController<NoParameterlessConstructorController>
                        .Instance()
                        .WithServices(services => services
                            .With(new RequestModel())
                            .With(new ResponseModel())
                            .With<IAnotherInjectedService>(new AnotherInjectedService()))
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking RequestModel, ResponseModel, AnotherInjectedService as parameters.");
        }
        
        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependenciesForPocoController()
        {
            Test.AssertException<UnresolvedServicesException>(
                () =>
                {
                    MyController<NoParameterlessConstructorController>
                        .Instance()
                        .WithServices(services => services
                            .With(new RequestModel())
                            .With(new ResponseModel())
                            .With<IAnotherInjectedService>(new AnotherInjectedService()))
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking RequestModel, ResponseModel, AnotherInjectedService as parameters.");
        }

        [Fact]
        public void WithServiceSetupForShouldSetupScopedServiceCorrectly()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddScoped<IScopedService, ScopedService>();
                });

            MyController<ServicesController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IScopedService>()
                    .WithSetupFor<IScopedService>(s => s.Value = "Custom"))
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Custom");

            MyController<ServicesController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IScopedService>()
                    .WithSetupFor<IScopedService>(s => s.Value = "SecondCustom"))
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "SecondCustom");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceSetupForShouldSetupScopedServiceCorrectlyWithConstructorInjection()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddScoped<IScopedService, ScopedService>();
                });

            MyController<ScopedServiceController>
                .Instance()
                .WithServices(services => services
                    .WithSetupFor<IScopedService>(s => s.Value = "TestValue"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok()
                .WithModel("TestValue");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithServiceSetupForShouldThrowExceptionWithNoScopedService()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddSingleton<IScopedService, ScopedService>();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<ScopedServiceController>
                        .Instance()
                        .WithServices(services => services
                            .WithSetupFor<IScopedService>(s => s.Value = "TestValue"))
                        .Calling(c => c.Index())
                        .ShouldReturn()
                        .Ok()
                        .WithModel("TestValue");
                },
                "The 'WithSetupFor' method can be used only for services with scoped lifetime.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
