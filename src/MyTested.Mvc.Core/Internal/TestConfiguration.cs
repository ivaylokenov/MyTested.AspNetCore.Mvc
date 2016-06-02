namespace MyTested.Mvc.Internal
{
    using Microsoft.Extensions.Configuration;

    internal class TestConfiguration
    {
        private const string ApplicationNameConfigKey = "ApplicationName";
        private const string EnvironmentNameConfigKey = "Environment";
        private const string AutomaticStartupConfigKey = "AutomaticStartup";
        private const string FullStartupNameConfigKey = "FullStartupName";
        private const string LicenseConfigKey = "License";
        private const string LicensesConfigKey = "Licenses";

        private readonly IConfiguration configuration;

        private TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal string ApplicationName => this.configuration[ApplicationNameConfigKey];

        internal string EnvironmentName => this.configuration[EnvironmentNameConfigKey] ?? "Test";

        internal bool AutomaticStartup => this.configuration.GetValue(AutomaticStartupConfigKey, true);

        internal string FullStartupName => this.configuration[FullStartupNameConfigKey];

        internal string[] Licenses
        {
            get
            {
                var license = this.configuration[LicenseConfigKey];
                if (license != null)
                {
                    return new[] { license };
                }

                return this.configuration.GetValue<string[]>(LicensesConfigKey);
            }
        }

        internal static TestConfiguration With(IConfiguration configuration)
        {
            return new TestConfiguration(configuration);
        }
    }
}
