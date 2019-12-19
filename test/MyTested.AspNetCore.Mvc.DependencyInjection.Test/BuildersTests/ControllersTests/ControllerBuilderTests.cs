﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
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
                .WithDependencies(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjectsForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithDependencies(new RequestModel(), new AnotherInjectedService(), new InjectedService())
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
                        .WithDependencies(dependencies => dependencies
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
                .WithDependencies(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjects()
        {
            MyController<MvcController>
                .Instance()
                .WithDependencies(new RequestModel(), new AnotherInjectedService(), new InjectedService())
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
                        .WithDependencies(dependencies => dependencies
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
                        .WithDependencies(dependencies => dependencies
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
                        .WithDependencies(dependencies => dependencies
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
                .WithDependencies(dependencies => dependencies
                    .WithNo<IScopedService>()
                    .WithSetupFor<IScopedService>(s => s.Value = "Custom"))
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>(result => result
                    .Passing(r => r == "Custom"));

            MyController<ServicesController>
                .Instance()
                .WithDependencies(dependencies => dependencies
                    .WithNo<IScopedService>()
                    .WithSetupFor<IScopedService>(s => s.Value = "SecondCustom"))
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>(result => result
                    .Passing(r => r == "SecondCustom"));

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
                .WithDependencies(dependencies => dependencies
                    .WithSetupFor<IScopedService>(s => s.Value = "TestValue"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("TestValue"));

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
                        .WithDependencies(dependencies => dependencies
                            .WithSetupFor<IScopedService>(s => s.Value = "TestValue"))
                        .Calling(c => c.Index())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel("TestValue"));
                },
                "The 'WithSetupFor' method can be used only for services with scoped lifetime.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
