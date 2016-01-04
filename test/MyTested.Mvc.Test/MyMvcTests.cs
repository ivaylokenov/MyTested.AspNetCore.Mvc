namespace MyTested.Mvc.Tests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Setups.Services;
    using System.Linq;
    using Xunit;
    using Internal;
    using Microsoft.AspNet.Mvc.Internal;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNet.Mvc;
    using System;
    using Setups.Startups;

    [Collection(ServiceBasedTests)]
    public class MyMvcTests
    {
        public const string ServiceBasedTests = "ServiceBasedTests";

        // TODO: move tests

        //[Fact]
        //public void IsUsingShouldOverrideTheDefaultConfiguration()
        //{
        //    // run two test cases in order to check the configuration is global
        //    var configs = new List<HttpConfiguration>();
        //    for (int i = 0; i < 2; i++)
        //    {
        //        var controller = MyMvc.Controller<MvcController>().AndProvideTheController();
        //        var actualConfig = controller.Configuration;

        //        Assert.NotNull(actualConfig);
        //        configs.Add(actualConfig);
        //    }

        //    Assert.AreSame(configs[0], configs[1]);
        //}

        [Fact]
        public void UsesDefaultServicesShouldPopulateDefaultServices()
        {
            MyMvc.IsUsingDefaultServices();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);

            MyMvc.IsNotUsingServices();
        }

        [Fact]
        public void IsUsingShouldAddServices()
        {
            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsNotUsingServices();
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

            MyMvc.IsNotUsingServices();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithAction()
        {
            MyMvc.IsUsing<CustomStartup>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyMvc.IsNotUsingServices();
        }

        [Fact]
        public void IsUsingWithStartUpClassShouldWorkCorrectlyWithFunc()
        {
            MyMvc.IsUsing<CustomStartupWithBuiltProvider>();

            var injectedService = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedService);
            Assert.IsAssignableFrom(typeof(ReplaceableInjectedService), injectedService);

            MyMvc.IsNotUsingServices();
        }

        [Fact]
        public void IsUsingWithAdditionalServicesShouldUseThem()
        {
            MyMvc.IsUsing<CustomStartup>(services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
            });

            var injectedServices = TestServiceProvider.GetService<IInjectedService>();

            Assert.NotNull(injectedServices);;
            Assert.IsAssignableFrom(typeof(InjectedService), injectedServices);

            MyMvc.IsNotUsingServices();
        }

        [Fact]
        public void IsUsingWithWrongStartupClassShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(() =>
            {
                MyMvc.IsUsing<MvcController>();
            }, "The provided MvcController class should have method named 'ConfigureServices' with void or IServiceProvider return type.");
        }

        [Fact]
        public void IsNotUsingServicesShouldSetServicesToNull()
        {
            MyMvc.IsNotUsingServices();

            Assert.Null(TestServiceProvider.Current);
        }

        [Fact]
        public void IsUsingShouldRecreateServicesEverytimeItIsInvoked()
        {
            MyMvc.IsUsingDefaultServices();

            var markerService = TestServiceProvider.GetService<MvcMarkerService>();

            Assert.NotNull(markerService);

            MyMvc.IsNotUsingServices();

            Assert.Null(TestServiceProvider.Current);

            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesRegistrationAction());

            var injectedService = TestServiceProvider.GetService<IInjectedService>();
            var anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsUsingDefaultServices();

            Test.AssertException<NullReferenceException>(() =>
            {
                injectedService = TestServiceProvider.GetRequiredService<IInjectedService>();
            }, "IInjectedService could not be resolved from the services provider. Before running this test case, the service should be registered in the 'IsUsing' method and cannot be null.");

            MyMvc.IsUsing(TestObjectFactory.GetCustomServicesRegistrationAction());

            injectedService = TestServiceProvider.GetService<IInjectedService>();
            anotherInjectedService = TestServiceProvider.GetService<IAnotherInjectedService>();

            Assert.NotNull(injectedService);
            Assert.NotNull(anotherInjectedService);

            MyMvc.IsNotUsingServices();
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

            MyMvc.IsNotUsingServices();
        }

        [Fact]
        public void ControllerWithNoParameterlessConstructorAndNoServicesShouldThrowProperException()
        {
            MyMvc.IsUsingDefaultServices();

            Test.AssertException<UnresolvedDependenciesException>(() =>
            {
                MyMvc
                    .Controller<NoParameterlessConstructorController>()
                    .Calling(c => c.OkAction())
                    .ShouldReturn()
                    .Ok();
            }, "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking no parameters.");

            MyMvc.IsNotUsingServices();
        }

        //[Fact]
        //public void HandlerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfHandlerType()
        //{
        //    var handler = MyMvc
        //        .Handler<CustomMessageHandler>()
        //        .AndProvideTheHandler();

        //    Assert.NotNull(handler);
        //    Assert.IsAssignableFrom<CustomMessageHandler>(handler);
        //}

        //[Fact]
        //public void HandlerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfHandlerType()
        //{
        //    var handler = MyMvc.Handler(() => new CustomMessageHandler()).AndProvideTheHandler();

        //    Assert.NotNull(handler);
        //    Assert.IsAssignableFrom<CustomMessageHandler>(handler);
        //}

        //[Fact]
        //public void HandlerWithProvidedInstanceShouldPopulateCorrectInstanceOfHandlerType()
        //{
        //    var instance = new CustomMessageHandler();
        //    var controller = MyMvc.Handler(instance).AndProvideTheHandler();

        //    Assert.NotNull(controller);
        //    Assert.IsAssignableFrom<CustomMessageHandler>(controller);
        //}

        //[Fact]
        //public void IsRegisteredWithShouldWorkCorrectly()
        //{
        //    MyMvc.IsRegisteredWith(WebApiConfig.Register);

        //    Assert.NotNull(MyMvc.Configuration);
        //    Assert.Equal(1, MyMvc.Configuration.Routes.Count);

        //    MyMvc.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        //}

        //[Fact]
        //public void IsUsingDefaultConfigurationShouldWorkCorrectly()
        //{
        //    MyMvc.IsUsingDefaultHttpConfiguration();

        //    Assert.NotNull(MyMvc.Configuration);
        //    Assert.Equal(0, MyMvc.Configuration.Routes.Count);

        //    MyMvc.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        //}
    }
}
