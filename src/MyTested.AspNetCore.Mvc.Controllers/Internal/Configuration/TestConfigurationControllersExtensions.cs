namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    public static class TestConfigurationControllersExtensions
    {
        public static ControllersTestConfiguration Controllers(this TestConfiguration testConfiguration)
            => testConfiguration.GetConfiguration(() => new ControllersTestConfiguration(testConfiguration.Configuration));
    }
}
