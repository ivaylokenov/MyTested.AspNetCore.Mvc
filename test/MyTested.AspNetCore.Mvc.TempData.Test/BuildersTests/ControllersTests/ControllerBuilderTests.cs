namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups;
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
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.Equal(1, controller.TempData.Count);
                });
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectlyForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyController<FullPocoController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    Assert.Single(controller.CustomTempData);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
