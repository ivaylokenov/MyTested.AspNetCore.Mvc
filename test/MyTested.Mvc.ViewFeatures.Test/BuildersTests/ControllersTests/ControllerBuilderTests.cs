namespace MyTested.Mvc.Test.BuildersTests.ControllersTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValues()
        {
            MyMvc
                .Controller<MvcController>()
                .WithServiceFor<IInjectedService>(null)
                .WithServiceFor<RequestModel>(null)
                .WithServiceFor<IAnotherInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructor()
        {
            MyMvc
                .Controller<MvcController>()
                .WithServiceFor<IInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowException()
        {
            MyMvc
                .Controller<MvcController>()
                .WithNoServiceFor<IInjectedService>()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempData()
        {
            MyMvc
                .Controller<MvcController>()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(1, controller.TempData.Count);
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValuesForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithServiceFor<IInjectedService>(null)
                .WithServiceFor<RequestModel>(null)
                .WithServiceFor<IAnotherInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructorForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithServiceFor<IInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowExceptionForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithNoServiceFor<IInjectedService>()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectlyForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(1, controller.CustomTempData.Count);
                });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvided()
        {
            MyMvc.IsUsingDefaultConfiguration();

            MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvidedForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
