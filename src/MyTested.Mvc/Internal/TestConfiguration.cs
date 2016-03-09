namespace MyTested.Mvc.Internal
{
    using Microsoft.Extensions.Configuration;

    internal class TestConfiguration
    {
        private const string EnvironmentNameConfigKey = "Environment";
        private const string AutomaticStartupConfigKey = "AutomaticStartup";
        private const string FullStartupNameConfigKey = "FullStartupName";

        private readonly IConfiguration configuration;

        private TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal static TestConfiguration With(IConfiguration configuration)
        {
            return new TestConfiguration(configuration);
        }

        internal string EnvironmentName => this.configuration[EnvironmentNameConfigKey] ?? "Test";

        internal bool AutomaticStartup
        {
            get
            {
                var configValue = this.configuration[AutomaticStartupConfigKey] ?? "true";

                bool automaticStartup = true;
                bool.TryParse(configValue, out automaticStartup);

                return automaticStartup;
            }
        }

        internal string FullStartupName => this.configuration[FullStartupNameConfigKey];
    }
}
