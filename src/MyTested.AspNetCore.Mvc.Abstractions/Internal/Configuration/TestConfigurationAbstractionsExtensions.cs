namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    public static class TestConfigurationAbstractionsExtensions
    {
        public static GeneralTestConfiguration GetGeneralConfiguration(this TestConfiguration testConfiguration)
            => testConfiguration.GetConfiguration(() => new GeneralTestConfiguration(testConfiguration.Configuration));
    }
}
