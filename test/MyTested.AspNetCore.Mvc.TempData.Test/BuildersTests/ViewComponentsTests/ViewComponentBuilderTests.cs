namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithValidTempDataValueShouldPopulateTempDataCorrectly()
        {
            MyViewComponent<TempDataComponent>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("test", "value"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("value");
        }

        [Fact]
        public void WithInvalidTempDataValueShouldReturnViewWithNoModel()
        {
            MyViewComponent<TempDataComponent>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("invalid", "value"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View();
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
                    Assert.Single(viewComponent.ViewContext.TempData);
                });
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>());

            MyViewComponent<PocoViewComponent>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .InvokedWith(vc => vc.Invoke())
                .ShouldReturn()
                .Result("POCO")
                .ShouldPassForThe<PocoViewComponent>(viewComponent =>
                {
                    Assert.Single(viewComponent.Context.ViewContext.TempData);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
