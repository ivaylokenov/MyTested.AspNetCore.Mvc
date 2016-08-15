namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal;
    using Internal.Services;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Setups.Startups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void DefaultConfigurationShouldSetMockedTempDataProvider()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<MockedTempDataProvider>(tempDataProvider);
        }

        [Fact]
        public void ExplicitMockedTempDataProviderShouldOverrideIt()
        {
            MyApplication
                .StartsFrom<DataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceTempDataProvider();
                });

            var tempDataProvider = TestServiceProvider.GetService<ITempDataProvider>();

            Assert.NotNull(tempDataProvider);
            Assert.IsAssignableFrom<MockedTempDataProvider>(tempDataProvider);

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
