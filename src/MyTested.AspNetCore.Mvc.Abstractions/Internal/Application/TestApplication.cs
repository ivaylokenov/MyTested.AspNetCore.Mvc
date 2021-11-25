namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Configuration;
    using Microsoft.AspNetCore.Builder;
    using Server;
    using Services;

    public static partial class TestApplication
    {
        private static readonly object Sync;

        private static bool initialized;

        static TestApplication()
        {
            Sync = new object();

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
                    var noStartup = generalConfiguration.NoStartup;

                    var testStartupType = TryFindTestStartupType();
                    if (testStartupType == null && !noStartup)
                    {
                        testStartupType = TryFindWebStartupType();
                    }

                    if (testStartupType == null && !noStartup)
                    {
                        throw new InvalidOperationException($"{TestWebServer.Environment.EnvironmentName}Startup class could not be found at the root of the test project. Either add it or set '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.AutomaticStartupKey}' in the test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) to 'false'.");
                    }

                    if (testStartupType != null && noStartup)
                    {
                        throw new InvalidOperationException($"The test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) contained 'true' value for the '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.NoStartupKey}' option but {TestWebServer.Environment.EnvironmentName}Startup class was located at the root of the project. Either remove the class or change the option to 'false'.");
                    }

                    startupType = testStartupType;
                }
            }
        }
        
        private static void Initialize()
        {
            TestWebServer.EnsureTestAssembly();

            TestFramework.EnsureCorrectVersion(TestWebServer.GetDependencyContext());

            ValidateStartup();

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
            PluginsContainer.Reset();
        }
    }
}
