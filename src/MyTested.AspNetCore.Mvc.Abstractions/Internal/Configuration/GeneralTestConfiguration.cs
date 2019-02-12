namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class GeneralTestConfiguration : BaseConfiguration
    {
        internal const string PrefixKey = "General";

        internal const string AsynchronousTestsKey = "AsynchronousTests";
        internal const string TestAssemblyNameKey = "TestAssemblyName";
        internal const string WebAssemblyNameKey = "WebAssemblyName";
        internal const string NoStartupKey = "NoStartup";
        internal const string AutomaticStartupKey = "AutomaticStartup";
        internal const string AutomaticApplicationPartsKey = "AutomaticApplicationParts";
        internal const string StartupTypeKey = "StartupType";
        internal const string ApplicationNameKey = "ApplicationName";
        internal const string EnvironmentNameKey = "Environment";

        public GeneralTestConfiguration(IConfiguration configuration) 
            : base(configuration)
        {
        }

        protected override string Prefix => PrefixKey;

        public bool AsynchronousTests => this.GetValue(AsynchronousTestsKey, true);

        public string TestAssemblyName => this.GetValue(TestAssemblyNameKey);

        public string WebAssemblyName => this.GetValue(WebAssemblyNameKey);
        
        public bool AutomaticStartup => this.GetValue(AutomaticStartupKey, true);
        
        public bool AutomaticApplicationParts => this.GetValue(AutomaticApplicationPartsKey, true);

        public bool NoStartup => this.GetValue(NoStartupKey, false);

        public string StartupType => this.GetValue(StartupTypeKey);

        public string ApplicationName => this.GetValue(ApplicationNameKey);

        public string EnvironmentName => this.GetValue(EnvironmentNameKey, "Test");
    }
}
