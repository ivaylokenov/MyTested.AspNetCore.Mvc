namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal.Application;
    using Internal.Versioning;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void VersioningPluginShouldDecorateApplicationAndRoutingActionSelectors()
        {
            MyApplication.StartsFrom<TestStartup>();

            var actionSelector = Assert.IsType<ApiVersionAwareActionSelector>(TestApplication.Services.GetRequiredService<IActionSelector>());
            var routingActionSelector = Assert.IsType<ApiVersionAwareActionSelector>(TestApplication.RoutingServices.GetRequiredService<IActionSelector>());

            Assert.IsNotType<ApiVersionAwareActionSelector>(actionSelector.ActionSelector);
            Assert.IsNotType<ApiVersionAwareActionSelector>(routingActionSelector.ActionSelector);
        }

        [Fact]
        public void VersioningPluginShouldNotDecorateActionSelectorsWithoutApiVersioning()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            Assert.IsNotType<ApiVersionAwareActionSelector>(TestApplication.Services.GetRequiredService<IActionSelector>());
            Assert.IsNotType<ApiVersionAwareActionSelector>(TestApplication.RoutingServices.GetRequiredService<IActionSelector>());

            MyApplication.StartsFrom<TestStartup>();
        }
    }
}
