namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectly()
        {
            MyController<MvcController>
                .Instance()
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
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(1, controller.TempData.Count);
                });
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectlyForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyController<FullPocoController>
                .Instance()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyController<FullPocoController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(1, controller.CustomTempData.Count);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
