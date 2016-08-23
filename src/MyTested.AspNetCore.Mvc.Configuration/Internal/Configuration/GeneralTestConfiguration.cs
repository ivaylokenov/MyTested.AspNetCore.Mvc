namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class GeneralTestConfiguration : BaseConfiguration
    {
        private const string TestAssemblyNameConfigKey = "TestAssemblyName";
        private const string AutomaticStartupConfigKey = "AutomaticStartup";
        private const string FullStartupNameConfigKey = "FullStartupName";
        private const string ApplicationNameConfigKey = "ApplicationName";
        private const string EnvironmentNameConfigKey = "Environment";

        public GeneralTestConfiguration(IConfiguration configuration) 
            : base(configuration)
        {
            this.Prefix = "General";
        }
        
        public string TestAssemblyName => this.GetValue(TestAssemblyNameConfigKey);

        public string ApplicationName => this.GetValue(ApplicationNameConfigKey);

        public string EnvironmentName => this.GetValue(EnvironmentNameConfigKey, "Test");

        public bool AutomaticStartup => this.GetValue(AutomaticStartupConfigKey, true);

        public string FullStartupName => this.GetValue(FullStartupNameConfigKey);
    }
}
