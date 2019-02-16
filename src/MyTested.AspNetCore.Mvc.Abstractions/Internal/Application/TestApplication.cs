namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Configuration;
    using Licensing;
    using Microsoft.AspNetCore.Builder;
    using Services;
    using System.Globalization;
    using System.Threading.Tasks;
    using Server;

    public static partial class TestApplication
    {
        private static readonly object Sync;

        private static bool initialized;

        static TestApplication()
        {
            Sync = new object();

            NullHandler = c => Task.CompletedTask;

            TestWebServer.TryFindTestAssembly();
        }
        
        internal static Action<IApplicationBuilder> AdditionalApplicationConfiguration { get; set; }
        
        public static void TryInitialize()
        {
            lock (Sync)
            {
                if (initialized)
                {
                    return;
                }

                var generalConfiguration = ServerTestConfiguration.General;

                if (StartupType == null && generalConfiguration.AutomaticStartup)
                {
                    var testStartupType = TryFindTestStartupType() ?? TryFindWebStartupType();

                    var noStartup = generalConfiguration.NoStartup;

                    if (testStartupType == null && !noStartup)
                    {
                        throw new InvalidOperationException($"{TestWebServer.Environment.EnvironmentName}Startup class could not be found at the root of the test project. Either add it or set '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.AutomaticStartupKey}' in the test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) to 'false'.");
                    }

                    if (testStartupType != null && noStartup)
                    {
                        throw new InvalidOperationException($"The test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) contained 'true' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but {TestWebServer.Environment.EnvironmentName}Startup class was located at the root of the project. Either remove the class or change the option to 'false'.");
                    }

                    startupType = testStartupType;
                    Initialize();
                }
            }
        }
        
        private static void Initialize()
        {
            TestWebServer.EnsureTestAssembly();

            if (StartupType == null && !ServerTestConfiguration.General.NoStartup)
            {
                throw new InvalidOperationException($"The test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) contained 'false' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but a Startup class was not provided. Either add {TestWebServer.Environment.EnvironmentName}Startup class to the root of the test project or set it by calling 'StartsFrom<TStartup>()'. Additionally, if you do not want to use a global test application for all test cases in this project, you may change the test configuration option to 'true'.");
            }
            
            TestCounter.SetLicenseData(
                ServerTestConfiguration.Global.Licenses,
                DateTime.ParseExact(TestFramework.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                TestWebServer.TestAssemblyName);

            PluginsContainer.LoadPlugins(TestWebServer.GetDependencyContext());

            var serviceCollection = TestWebServer.GetInitialServiceCollection();

            PrepareStartup(serviceCollection);
            PrepareServices(serviceCollection);
            
            PrepareApplicationAndRouting();

            initialized = true;
        }

        private static void TryLockedInitialization()
        {
            if (!initialized)
            {
                lock (Sync)
                {
                    if (!initialized)
                    {
                        Initialize();
                    }
                }
            }
        }
        
        private static void Reset()
        {
            initialized = false;
            startupType = null;
            startupMethods = null;
            serviceProvider = null;
            routingServiceProvider = null;
            router = null;
            AdditionalServices = null;
            AdditionalApplicationConfiguration = null;
            AdditionalRouting = null;
            TestServiceProvider.Current = null;
            TestServiceProvider.ClearServiceLifetimes();
            LicenseValidator.ClearLicenseDetails();
            PluginsContainer.Reset();
        }
    }
}
