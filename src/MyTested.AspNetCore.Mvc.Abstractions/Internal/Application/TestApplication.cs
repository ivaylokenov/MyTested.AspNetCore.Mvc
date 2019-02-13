namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Configuration;
    using Licensing;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Plugins;
    using Services;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    public static partial class TestApplication
    {
        private const string TestFrameworkName = "MyTested.AspNetCore.Mvc";
        private const string ReleaseDate = "2019-03-01";

        private static readonly object Sync;

        private static bool initialiazed;

        private static IHostingEnvironment environment;

        static TestApplication()
        {
            Sync = new object();

            NullHandler = c => Task.CompletedTask;

            DefaultRegistrationPlugins = new HashSet<IDefaultRegistrationPlugin>();
            ServiceRegistrationPlugins = new HashSet<IServiceRegistrationPlugin>();
            RoutingServiceRegistrationPlugins = new HashSet<IRoutingServiceRegistrationPlugin>();
            InitializationPlugins = new HashSet<IInitializationPlugin>();

            TryFindTestAssembly();
        }
        
        internal static Action<IApplicationBuilder> AdditionalApplicationConfiguration { get; set; }

        internal static IHostingEnvironment Environment
        {
            get
            {
                if (environment == null)
                {
                    environment = PrepareEnvironment();
                }

                return environment;
            }
        }

        internal static string ApplicationName
            => GeneralConfiguration.ApplicationName ?? WebAssemblyName ?? TestAssemblyName;
        
        public static void TryInitialize()
        {
            lock (Sync)
            {
                if (initialiazed)
                {
                    return;
                }

                var initializationConfiguration = GeneralConfiguration;

                if (StartupType == null && initializationConfiguration.AutomaticStartup)
                {
                    var testStartupType = TryFindTestStartupType() ?? TryFindWebStartupType();

                    var noStartup = initializationConfiguration.NoStartup;

                    if (testStartupType == null && !noStartup)
                    {
                        throw new InvalidOperationException($"{Environment.EnvironmentName}Startup class could not be found at the root of the test project. Either add it or set '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.AutomaticStartupKey}' in the test configuration ('{DefaultConfigurationFile}' file by default) to 'false'.");
                    }

                    if (testStartupType != null && noStartup)
                    {
                        throw new InvalidOperationException($"The test configuration ('{DefaultConfigurationFile}' file by default) contained 'true' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but {Environment.EnvironmentName}Startup class was located at the root of the project. Either remove the class or change the option to 'false'.");
                    }

                    startupType = testStartupType;
                    Initialize();
                }
            }
        }
        
        private static void Initialize()
        {
            EnsureTestAssembly();

            if (StartupType == null && !GeneralConfiguration.NoStartup)
            {
                throw new InvalidOperationException($"The test configuration ('{DefaultConfigurationFile}' file by default) contained 'false' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but a Startup class was not provided. Either add {Environment.EnvironmentName}Startup class to the root of the test project or set it by calling 'StartsFrom<TStartup>()'. Additionally, if you do not want to use a global test application for all test cases in this project, you may change the test configuration option to 'true'.");
            }

            PrepareLicensing();

            LoadPlugins();

            var serviceCollection = GetInitialServiceCollection();
            var startupMethods = PrepareStartup(serviceCollection);

            PrepareServices(serviceCollection, startupMethods);
            PrepareApplicationAndRouting(startupMethods);

            initialiazed = true;
        }

        private static void PrepareLicensing()
        {
            if (TestAssembly != null)
            {
                TestCounter.SetLicenseData(
                    TestConfiguration.Licenses,
                    DateTime.ParseExact(ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    TestAssemblyName);
            }
        }

        private static IHostingEnvironment PrepareEnvironment()
            => new HostingEnvironment
            {
                ApplicationName = ApplicationName,
                EnvironmentName = GeneralConfiguration.EnvironmentName,
                ContentRootPath = AppContext.BaseDirectory
            };
        
        private static void TryLockedInitialization()
        {
            if (!initialiazed)
            {
                lock (Sync)
                {
                    if (!initialiazed)
                    {
                        Initialize();
                    }
                }
            }
        }
        
        private static void Reset()
        {
            initialiazed = false;
            dependencyContext = null;
            projectLibraries = null;
            configurationBuilder = null;
            configuration = null;
            environment = null;
            startupType = null;
            serviceProvider = null;
            routingServiceProvider = null;
            router = null;
            testAssembly = null;
            webAssembly = null;
            testAssemblyScanned = false;
            webAssemblyScanned = false;
            testAssemblyName = null;
            webAssemblyName = null;
            AdditionalServices = null;
            AdditionalApplicationConfiguration = null;
            AdditionalRouting = null;
            DefaultRegistrationPlugins.Clear();
            ServiceRegistrationPlugins.Clear();
            RoutingServiceRegistrationPlugins.Clear();
            InitializationPlugins.Clear();
            TestServiceProvider.Current = null;
            TestServiceProvider.ClearServiceLifetimes();
            LicenseValidator.ClearLicenseDetails();
        }
    }
}
