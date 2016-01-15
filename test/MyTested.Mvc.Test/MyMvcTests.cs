using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MyTested.Mvc.Tests
{
    using System;
    using System.Linq;
    using Exceptions;
    using Internal;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Setups;
    using Setups.Controllers;
    using Setups.Services;
    using Setups.Startups;

    public class MyMvcTests
    {
        [Fact]
        public void UsesDefaultServicesShouldPopulateDefaultServices()
        {
            MyMvc.IsUsingDefaultServices();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);
        }

        [Fact]
        public void IsUsingShouldAddServices()
        {
            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultServices();
        }

        [Fact]
        public void IsUsingShouldAddServicesWithOptions()
        {
            MyMvc.IsUsingDefaultServices();

            var initialSetOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesWithOptionsRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            var setOptions = TestServiceProvider.GetServices<IConfigureOptions<MvcOptions>>();

            Assert.NotNull(setOptions);
            Assert.Equal(initialSetOptions.Count() + 1, setOptions.Count());

            MyMvc.IsUsingDefaultServices();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithAction()
        {
            MyMvc.IsUsing<CustomStartup>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyMvc.IsUsingDefaultServices();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithFunc()
        {
            MyMvc.IsUsing<CustomStartupWithBuiltProvider>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyMvc.IsUsingDefaultServices();
        }

        [Fact]
        public void IsUsingWithAdditionalServicesShouldUseThem()
        {
            MyMvc.IsUsing<CustomStartup>(services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
            });

            var injectedServices = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedServices);
            Assert.IsAssignableFrom(typeof(InjectedService), injectedServices);

            MyMvc.IsUsingDefaultServices();
        }

        [Fact]
        public void IsUsingWithWrongStartupClassShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc.IsUsing<MvcController>();
                },
                "The provided MvcController class should have method named 'ConfigureServices' with void or IServiceProvider return type.");
        }

        [Fact]
        public void IsUsingShouldRecreateServicesEverytimeItIsInvoked()
        {
            MyMvc.IsUsingDefaultServices();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);

            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultServices();

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    injectedService = TestServiceProvider.GetRequiredService<IInjectedService>();
                },
                "IInjectedService could not be resolved from the services provider. Before running this test case, the service should be registered in the 'IsUsing' method and cannot be null.");

            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesRegistrationAction());

            injectedService = TestServiceProvider.GetService<IInjectedService>();
            anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultServices();
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
            MyMvc.IsUsing(services =>
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

            MyMvc.IsUsingDefaultServices();
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndNoServicesShouldThrowProperException()
        {
            MyMvc.IsUsingDefaultServices();

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

            MyMvc.IsUsingDefaultServices();
        }
    }
}
