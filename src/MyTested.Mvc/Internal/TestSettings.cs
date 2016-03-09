namespace MyTested.Mvc.Internal
{
    using Microsoft.Extensions.Configuration;

    internal class TestSettings
    {
        private const string EnvironmentNameConfigKey = "Environment";
        private const string AutomaticStartupConfigKey = "AutomaticStartup";

        private readonly IConfiguration configuration;
        
        private TestSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal static TestSettings With(IConfiguration configuration)
        {
            return new TestSettings(configuration);
        }

        internal string EnvironmentName
        {
            get
            {
                return this.configuration[EnvironmentNameConfigKey] ?? "Tests";
            }
        }

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
    }
}
