namespace MyTested.Mvc.Test
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Services;
    using Xunit;
    
    public class MyMvcTests
    {
        // TODO: move tests

        //[Fact]
        //public void IsUsingShouldOverrideTheDefaultConfiguration()
        //{
        //    // run two test cases in order to check the configuration is global
        //    var configs = new List<HttpConfiguration>();
        //    for (int i = 0; i < 2; i++)
        //    {
        //        var controller = MyWebApi.Controller<WebApiController>().AndProvideTheController();
        //        var actualConfig = controller.Configuration;

        //        Assert.NotNull(actualConfig);
        //        configs.Add(actualConfig);
        //    }

        //    Assert.AreSame(configs[0], configs[1]);
        //}

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

        // TODO: ?
        //[Fact]
        //[ExpectedException(
        //    typeof(UnresolvedDependenciesException),
        //    ExpectedMessage = "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking no parameters.")]
        //public void ControllerWithNoParameterlessConstructorShouldThrowProperException()
        //{
        //    MyMvc
        //        .Controller<NoParameterlessConstructorController>()
        //        .Calling(c => c.OkAction())
        //        .ShouldReturn()
        //        .Ok();
        //}

        //[Fact]
        //public void HandlerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfHandlerType()
        //{
        //    var handler = MyWebApi
        //        .Handler<CustomMessageHandler>()
        //        .AndProvideTheHandler();

        //    Assert.NotNull(handler);
        //    Assert.IsAssignableFrom<CustomMessageHandler>(handler);
        //}

        //[Fact]
        //public void HandlerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfHandlerType()
        //{
        //    var handler = MyWebApi.Handler(() => new CustomMessageHandler()).AndProvideTheHandler();

        //    Assert.NotNull(handler);
        //    Assert.IsAssignableFrom<CustomMessageHandler>(handler);
        //}

        //[Fact]
        //public void HandlerWithProvidedInstanceShouldPopulateCorrectInstanceOfHandlerType()
        //{
        //    var instance = new CustomMessageHandler();
        //    var controller = MyWebApi.Handler(instance).AndProvideTheHandler();

        //    Assert.NotNull(controller);
        //    Assert.IsAssignableFrom<CustomMessageHandler>(controller);
        //}

        //[Fact]
        //public void IsRegisteredWithShouldWorkCorrectly()
        //{
        //    MyWebApi.IsRegisteredWith(WebApiConfig.Register);

        //    Assert.NotNull(MyWebApi.Configuration);
        //    Assert.AreEqual(1, MyWebApi.Configuration.Routes.Count);

        //    MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        //}

        //[Fact]
        //public void IsUsingDefaultConfigurationShouldWorkCorrectly()
        //{
        //    MyWebApi.IsUsingDefaultHttpConfiguration();

        //    Assert.NotNull(MyWebApi.Configuration);
        //    Assert.AreEqual(0, MyWebApi.Configuration.Routes.Count);

        //    MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        //}
    }
}
