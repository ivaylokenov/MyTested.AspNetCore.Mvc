namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectly()
        {
            MyViewComponent<TempDataComponent>
                .Instance()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("value");
        }

        [Fact]
        public void WithTempDataShouldPopulateTempData()
        {
            MyViewComponent<TempDataComponent>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassForThe<TempDataComponent>(viewComponent =>
                {
                    Assert.Equal(1, viewComponent.ViewContext.TempData.Count);
                });
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

            MyViewComponent<PocoViewComponent>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .InvokedWith(vc => vc.Invoke())
                .ShouldReturn()
                .Result("POCO")
                .ShouldPassForThe<PocoViewComponent>(viewComponent =>
                {
                    Assert.Equal(1, viewComponent.Context.ViewContext.TempData.Count);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
