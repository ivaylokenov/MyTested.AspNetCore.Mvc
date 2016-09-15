namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal;
    using Internal.Services;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Setups;
    using Setups.Startups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void DefaultConfigurationShouldSetMockTempDataProvider()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<TempDataProviderMock>(tempDataProvider);
        }

        [Fact]
        public void ExplicitMockTempDataProviderShouldOverrideIt()
        {
            MyApplication
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceTempDataProvider();
                });

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<TempDataProviderMock>(tempDataProvider);

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
