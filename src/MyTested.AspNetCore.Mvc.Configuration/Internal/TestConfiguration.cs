namespace MyTested.AspNetCore.Mvc.Internal
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public class TestConfiguration
    {
        private const string TestAssemblyNameConfigKey = "TestAssemblyName";
        private const string ApplicationNameConfigKey = "ApplicationName";
        private const string EnvironmentNameConfigKey = "Environment";
        private const string AutomaticStartupConfigKey = "AutomaticStartup";
        private const string FullStartupNameConfigKey = "FullStartupName";
        private const string ModelStateValidationConfigKey = "ModelStateValidation";
        private const string LicenseConfigKey = "License";
        private const string LicensesConfigKey = "Licenses";

        private readonly IConfiguration configuration;

        private TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string TestAssemblyName => this.configuration[TestAssemblyNameConfigKey];

        public string ApplicationName => this.configuration[ApplicationNameConfigKey];

        public string EnvironmentName => this.configuration[EnvironmentNameConfigKey] ?? "Test";

        public bool AutomaticStartup => this.configuration.GetValue(AutomaticStartupConfigKey, true);

        public string FullStartupName => this.configuration[FullStartupNameConfigKey];

        public bool ModelStateValidation => this.configuration.GetValue(ModelStateValidationConfigKey, true);

        public IEnumerable<string> Licenses
        {
            get
            {
                var license = this.configuration[LicenseConfigKey];
                if (license != null)
                {
                    return new[] { license };
                }

                var licenses = new List<string>();
                this.configuration.GetSection(LicensesConfigKey).Bind(licenses);
                return licenses;
            }
        }

        public static TestConfiguration With(IConfiguration configuration)
        {
            return new TestConfiguration(configuration);
        }
    }
}
