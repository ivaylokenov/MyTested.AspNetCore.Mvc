namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    public static class TestConfigurationAbstractionsExtensions
    {
        public static GeneralTestConfiguration General(this TestConfiguration testConfiguration)
            => testConfiguration.GetConfiguration(() => new GeneralTestConfiguration(testConfiguration.Configuration));
    }
}
