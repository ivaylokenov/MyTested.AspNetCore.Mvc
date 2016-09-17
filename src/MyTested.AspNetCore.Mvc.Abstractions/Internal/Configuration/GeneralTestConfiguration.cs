namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class GeneralTestConfiguration : BaseConfiguration
    {
        private const string AsynchronousTestsConfigKey = "AsynchronousTests";
        private const string WebAssemblyNameConfigKey = "WebAssemblyName";
        private const string TestAssemblyNameConfigKey = "TestAssemblyName";
        private const string NoStartupConfigKey = "NoStartup";
        private const string AutomaticStartupConfigKey = "AutomaticStartup";
        private const string StartupTypeConfigKey = "StartupType";
        private const string ApplicationNameConfigKey = "ApplicationName";
        private const string EnvironmentNameConfigKey = "Environment";

        public GeneralTestConfiguration(IConfiguration configuration) 
            : base(configuration)
        {
            this.Prefix = "General";
        }

        public bool AsynchronousTests() => this.GetValue(AsynchronousTestsConfigKey, true);

        public string WebAssemblyName() => this.GetValue(WebAssemblyNameConfigKey);
        
        public string TestAssemblyName() => this.GetValue(TestAssemblyNameConfigKey);

        public bool AutomaticStartup() => this.GetValue(AutomaticStartupConfigKey, true);

        public bool NoStartup() => this.GetValue(NoStartupConfigKey, false);

        public string StartupType() => this.GetValue(StartupTypeConfigKey);

        public string ApplicationName() => this.GetValue(ApplicationNameConfigKey);

        public string EnvironmentName() => this.GetValue(EnvironmentNameConfigKey, "Test");
    }
}
