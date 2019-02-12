namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    public static class TestConfigurationControllersExtensions
    {
        public static ControllersTestConfiguration GetControllersConfiguration(this TestConfiguration testConfiguration)
            => testConfiguration.GetConfiguration(() => new ControllersTestConfiguration(testConfiguration.Configuration));
    }
}
