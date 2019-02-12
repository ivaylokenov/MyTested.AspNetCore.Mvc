namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Abstractions.Utilities.Extensions;
    using Configuration;
    using Licensing;
    using Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Builder;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.DotNet.PlatformAbstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.DependencyModel;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;
    using Plugins;
    using Services;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Abstractions.Internal.Application;
    using Utilities.Extensions;

    public static class TestApplication
    {
        private const string TestFrameworkName = "MyTested.AspNetCore.Mvc";
        private const string ReleaseDate = "2019-03-01";

        private const string DefaultConfigurationFile = "testsettings.json";

        private const string AspNetCoreMetaPackageName = "Microsoft.AspNetCore.App";

        private const string DefaultStartupTypeName = "Startup";

        private static readonly object Sync;

        private static readonly RequestDelegate NullHandler;

        private static readonly ISet<IDefaultRegistrationPlugin> DefaultRegistrationPlugins;
        private static readonly ISet<IServiceRegistrationPlugin> ServiceRegistrationPlugins;
        private static readonly ISet<IRoutingServiceRegistrationPlugin> RoutingServiceRegistrationPlugins;
        private static readonly ISet<IInitializationPlugin> InitializationPlugins;

        private static bool initialiazed;

        private static DependencyContext dependencyContext;
        private static IEnumerable<RuntimeLibrary> projectLibraries;

        private static Assembly testAssembly;
        private static Assembly webAssembly;
        private static bool testAssemblyScanned;
        private static bool webAssemblyScanned;
        private static string testAssemblyName;
        private static string webAssemblyName;

        private static IConfigurationBuilder configurationBuilder;
        private static TestConfiguration configuration;
        private static GeneralTestConfiguration generalConfiguration;

        private static IHostingEnvironment environment;

        private static Type startupType;

        private static volatile IServiceProvider serviceProvider;
        private static volatile IServiceProvider routingServiceProvider;
        private static volatile IRouter router;

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

        public static TestConfiguration TestConfiguration
        {
            get
            {
                if (configuration == null || AdditionalConfiguration != null)
                {
                    if (configurationBuilder == null)
                    {
                        configurationBuilder = new ConfigurationBuilder()
                            .AddJsonFile(DefaultConfigurationFile, optional: true)
                            .AddJsonFile("testconfig.json", optional: true); // For backwards compatibility.
                    }

                    AdditionalConfiguration?.Invoke(configurationBuilder);
                    AdditionalConfiguration = null;

                    configuration = TestConfiguration.With(configurationBuilder.Build());
                    generalConfiguration = null;

                    PrepareLicensing();
                }

                return configuration;
            }
        }

        public static IServiceProvider Services
        {
            get
            {
                TryLockedInitialization();
                return serviceProvider;
            }
        }

        public static IServiceProvider RoutingServices
        {
            get
            {
                TryLockedInitialization();
                return routingServiceProvider;
            }
        }

        public static IRouter Router
        {
            get
            {
                TryLockedInitialization();
                return router;
            }
        }

        internal static Assembly TestAssembly
        {
            private get
            {
                if (testAssembly == null)
                {
                    TryFindTestAssembly();
                }

                return testAssembly;
            }

            set => testAssembly = value;
        }

        internal static Assembly WebAssembly
        {
            private get
            {
                if (webAssembly == null)
                {
                    TryFindWebAssembly();
                }

                return webAssembly;
            }

            set => webAssembly = value;
        }

        internal static string TestAssemblyName
        {
            get
            {
                if (testAssemblyName == null)
                {
                    if (testAssembly != null)
                    {
                        testAssemblyName = testAssembly.GetShortName();
                    }
                    else
                    {
                        TryFindTestAssembly();
                    }
                }

                return testAssemblyName;
            }
        }

        internal static string WebAssemblyName
        {
            get
            {
                if (webAssemblyName == null)
                {
                    if (webAssembly != null)
                    {
                        webAssemblyName = webAssembly.GetShortName();
                    }
                    else
                    {
                        TryFindWebAssembly();
                    }
                }

                return webAssemblyName;
            }
        }

        internal static GeneralTestConfiguration GeneralConfiguration
        {
            get
            {
                if (generalConfiguration == null)
                {
                    generalConfiguration = TestConfiguration.GetGeneralConfiguration();
                }

                return generalConfiguration;
            }
        }

        internal static Type StartupType
        {
            get => startupType;

            set
            {
                if (value != null && GeneralConfiguration.NoStartup)
                {
                    throw new InvalidOperationException($"The test configuration ('{DefaultConfigurationFile}' file by default) contained 'true' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but {value.GetName()} class was set through the 'StartsFrom<TStartup>()' method. Either do not set the class or change the option to 'false'.");
                }

                if (startupType != null && GeneralConfiguration.AsynchronousTests)
                {
                    throw new InvalidOperationException($"Multiple Startup types per test project while running asynchronous tests is not supported. Either set '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.AsynchronousTestsKey}' in the test configuration ('{DefaultConfigurationFile}' file by default) to 'false' or separate your tests into different test projects. The latter is recommended. If you choose the first option, you may need to disable asynchronous testing in your preferred test runner too.");
                }

                if (initialiazed)
                {
                    Reset();
                }

                startupType = value;
            }
        }

        internal static Action<IConfigurationBuilder> AdditionalConfiguration { get; set; }

        internal static Action<IServiceCollection> AdditionalServices { get; set; }

        internal static Action<IApplicationBuilder> AdditionalApplicationConfiguration { get; set; }

        internal static Action<IRouteBuilder> AdditionalRouting { get; set; }

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
        
        private static IEnumerable<RuntimeLibrary> ProjectLibraries
        {
            get
            {
                if (projectLibraries == null)
                {
                    projectLibraries = GetDependencyContext()
                        .RuntimeLibraries
                        .Where(l => l.Type == "project");
                }

                return projectLibraries;
            }
        }

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

        internal static void LoadPlugins()
        {
            var testFrameworkAssemblies = GetDependencyContext()
                .GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier())
                .Where(l => l.Name.StartsWith(TestFrameworkName))
                .ToArray();

            if (testFrameworkAssemblies.Length == 7 && testFrameworkAssemblies.Any(t => t.Name == $"{TestFrameworkName}.Lite"))
            {
                TestCounter.SkipValidation = true;
            }

            var plugins = testFrameworkAssemblies
                .Select(l => Assembly
                    .Load(new AssemblyName(l.Name))
                    .GetType($"{TestFrameworkName}.Plugins.{l.Name.Replace(TestFrameworkName, string.Empty).Trim('.')}TestPlugin"))
                .Where(p => p != null)
                .ToArray();

            if (!plugins.Any())
            {
                throw new InvalidOperationException("Test plugins could not be loaded. Depending on your project's configuration you may need to set '<PreserveCompilationContext>true</PreserveCompilationContext>' in the test assembly's '.csproj' file and/or may need to call '.StartsFrom<TStartup>().WithTestAssembly(this)'.");
            }

            plugins.ForEach(t =>
            {
                var plugin = Activator.CreateInstance(t);

                if (plugin is IDefaultRegistrationPlugin defaultRegistrationPlugin)
                {
                    DefaultRegistrationPlugins.Add(defaultRegistrationPlugin);
                }

                if (plugin is IServiceRegistrationPlugin servicePlugin)
                {
                    ServiceRegistrationPlugins.Add(servicePlugin);
                }

                if (plugin is IRoutingServiceRegistrationPlugin routingServicePlugin)
                {
                    RoutingServiceRegistrationPlugins.Add(routingServicePlugin);
                }

                if (plugin is IInitializationPlugin initializationPlugin)
                {
                    InitializationPlugins.Add(initializationPlugin);
                }

                if (plugin is IHttpFeatureRegistrationPlugin httpFeatureRegistrationPlugin)
                {
                    TestHelper.HttpFeatureRegistrationPlugins.Add(httpFeatureRegistrationPlugin);
                }

                if (plugin is IShouldPassForPlugin shouldPassForPlugin)
                {
                    TestHelper.ShouldPassForPlugins.Add(shouldPassForPlugin);
                }
            });
        }

        internal static Type TryFindTestStartupType()
        {
            EnsureTestAssembly();

            var defaultTestStartupType = GeneralConfiguration.StartupType ?? $"{Environment.EnvironmentName}{DefaultStartupTypeName}";

            // Check root of the test project.
            return
                TestAssembly.GetType(defaultTestStartupType) ??
                TestAssembly.GetType($"{TestAssemblyName}.{defaultTestStartupType}");
        }

        internal static Type TryFindWebStartupType()
        {
            if (WebAssembly == null)
            {
                return null;
            }

            // Check root of the test project.
            return
                WebAssembly.GetType(DefaultStartupTypeName) ??
                WebAssembly.GetType($"{WebAssemblyName}.{DefaultStartupTypeName}");
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

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var diagnosticListener = new DiagnosticListener(TestFrameworkName);

            // Default server services.
            serviceCollection.AddSingleton(Environment);
            serviceCollection.AddSingleton<IApplicationLifetime, ApplicationLifetime>();

            serviceCollection.AddTransient<IApplicationBuilderFactory, ApplicationBuilderFactory>();
            serviceCollection.AddTransient<IHttpContextFactory, HttpContextFactory>();
            serviceCollection.AddScoped<IMiddlewareFactory, MiddlewareFactory>();
            serviceCollection.AddOptions();

            serviceCollection.AddSingleton<ILoggerFactory>(LoggerFactoryMock.Create());
            serviceCollection.AddLogging();

            serviceCollection.AddSingleton(TestConfiguration.Configuration);

            serviceCollection.AddSingleton(diagnosticListener);
            serviceCollection.AddSingleton<DiagnosticSource>(diagnosticListener);

            serviceCollection.AddTransient<IStartupFilter, AutoRequestServicesStartupFilter>();
            serviceCollection.AddTransient<IServiceProviderFactory<IServiceCollection>, DefaultServiceProviderFactory>();

            serviceCollection.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            return serviceCollection;
        }

        private static StartupMethods PrepareStartup(IServiceCollection serviceCollection)
        {
            StartupMethods startupMethods = null;
            if (StartupType != null)
            {
                startupMethods = StartupLoader.LoadMethods(
                    serviceCollection.BuildServiceProvider(),
                    StartupType,
                    Environment.EnvironmentName);

                if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(StartupType.GetTypeInfo()))
                {
                    serviceCollection.AddSingleton(typeof(IStartup), StartupType);
                }
                else
                {
                    serviceCollection.AddSingleton(typeof(IStartup), sp => new ConventionBasedStartup(startupMethods));
                }
            }

            return startupMethods;
        }

        private static void PrepareServices(IServiceCollection serviceCollection, StartupMethods startupMethods)
        {
            if (startupMethods?.ConfigureServicesDelegate != null)
            {
                PrepareStartupServices(serviceCollection, startupMethods);
            }
            else
            {
                PrepareDefaultServices(serviceCollection);
            }

            AdditionalServices?.Invoke(serviceCollection);

            TryReplaceKnownServices(serviceCollection);
            PrepareRoutingServices(serviceCollection);
            EnsureApplicationParts(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            InitializationPlugins.ForEach(plugin => plugin.InitializationDelegate(serviceProvider));
        }

        private static void PrepareStartupServices(IServiceCollection serviceCollection, StartupMethods startupMethods)
        {
            try
            {
                startupMethods.ConfigureServicesDelegate(serviceCollection);
            }
            catch
            {
                throw new InvalidOperationException($"Application dependencies could not be loaded correctly. If your web project references the '{AspNetCoreMetaPackageName}' package, you need to reference it in your test project too. Additionally, make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file.");
            }
        }

        private static void PrepareDefaultServices(IServiceCollection serviceCollection)
        {
            var defaultRegistrationPlugin = DefaultRegistrationPlugins
                .OrderByDescending(p => p.Priority)
                .FirstOrDefault();

            if (defaultRegistrationPlugin != null)
            {
                defaultRegistrationPlugin.DefaultServiceRegistrationDelegate(serviceCollection);
            }
            else
            {
                serviceCollection.AddMvcCore();
            }
        }

        private static void TryReplaceKnownServices(IServiceCollection serviceCollection)
        {
            var applicablePlugins = new HashSet<IServiceRegistrationPlugin>();

            serviceCollection.ForEach(service =>
            {
                TestServiceProvider.SaveServiceLifetime(service.ServiceType, service.Lifetime);

                foreach (var serviceRegistrationPlugin in ServiceRegistrationPlugins)
                {
                    if (serviceRegistrationPlugin.ServiceSelectorPredicate(service))
                    {
                        applicablePlugins.Add(serviceRegistrationPlugin);
                    }
                }
            });

            foreach (var applicablePlugin in applicablePlugins)
            {
                applicablePlugin.ServiceRegistrationDelegate(serviceCollection);
            }
        }

        private static void PrepareRoutingServices(IServiceCollection serviceCollection)
        {
            var routingServiceCollection = new ServiceCollection().Add(serviceCollection);

            foreach (var routingServiceRegistrationPlugin in RoutingServiceRegistrationPlugins)
            {
                routingServiceRegistrationPlugin.RoutingServiceRegistrationDelegate(routingServiceCollection);
            }

            routingServiceProvider = routingServiceCollection.BuildServiceProvider();
        }

        private static void EnsureApplicationParts(IServiceCollection serviceCollection)
        {
            var baseStartupTypeAssembly = WebAssembly;
            if (baseStartupTypeAssembly == null)
            {
                var baseStartupType = StartupType;
                while (baseStartupType != null && baseStartupType.BaseType != typeof(object))
                {
                    baseStartupType = baseStartupType.BaseType;
                }
                
                baseStartupTypeAssembly = baseStartupType?.GetTypeInfo().Assembly;
            }
            
            var applicationPartManager = (ApplicationPartManager)serviceCollection
                .FirstOrDefault(t => t.ServiceType == typeof(ApplicationPartManager))
                ?.ImplementationInstance;

            if (applicationPartManager != null && baseStartupTypeAssembly != null)
            {
                var baseStartupTypeAssemblyName = baseStartupTypeAssembly.GetShortName();

                if (applicationPartManager.ApplicationParts.All(a => a.Name != baseStartupTypeAssemblyName))
                {
                    throw new InvalidOperationException($"Web application {baseStartupTypeAssemblyName} could not be loaded correctly. Make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file. Additionally, if your web project references the '{AspNetCoreMetaPackageName}' package, you need to reference it in your test project too.");
                }

                if (GeneralConfiguration.AutomaticApplicationParts)
                {
                    var testAssemblyParts = TestApplicationPartsProvider.GetTestAssemblyParts(
                        ProjectLibraries,
                        TestAssemblyName,
                        applicationPartManager.ApplicationParts.Select(a => a.Name));

                    testAssemblyParts.ForEach(ap => applicationPartManager.ApplicationParts.Add(ap));
                }
            }
        }

        private static void PrepareApplicationAndRouting(StartupMethods startupMethods)
        {
            var applicationBuilder = new ApplicationBuilderMock(routingServiceProvider);

            startupMethods?.ConfigureDelegate?.Invoke(applicationBuilder);

            AdditionalApplicationConfiguration?.Invoke(applicationBuilder);

            var routeBuilder = new RouteBuilder(applicationBuilder)
            {
                DefaultHandler = new RouteHandler(NullHandler)
            };

            for (int i = 0; i < applicationBuilder.Routes.Count; i++)
            {
                var route = applicationBuilder.Routes[i];
                routeBuilder.Routes.Add(route);
            }

            AdditionalRouting?.Invoke(routeBuilder);

            if (StartupType == null || routeBuilder.Routes.Count == 0)
            {
                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routeBuilder.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(serviceProvider));
            }

            router = routeBuilder.Build();
        }

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
            TestServiceProvider.Current = null;
            TestServiceProvider.ClearServiceLifetimes();
            DefaultRegistrationPlugins.Clear();
            ServiceRegistrationPlugins.Clear();
            RoutingServiceRegistrationPlugins.Clear();
            InitializationPlugins.Clear();
            LicenseValidator.ClearLicenseDetails();
        }

        private static DependencyContext GetDependencyContext()
        {
            if (dependencyContext == null)
            {
                dependencyContext = DependencyContext.Load(TestAssembly) ?? DependencyContext.Default;
            }

            return dependencyContext;
        }

        private static void TryFindTestAssembly()
        {
            if (testAssembly != null || testAssemblyScanned)
            {
                return;
            }

            var testAssemblyNameFromConfiguration = GeneralConfiguration.TestAssemblyName;
            if (testAssemblyNameFromConfiguration != null)
            {
                try
                {
                    testAssemblyName = testAssemblyNameFromConfiguration;
                    testAssembly = Assembly.Load(new AssemblyName(testAssemblyName));
                }
                catch
                {
                    throw new InvalidOperationException($"Test assembly could not be loaded. The provided '{testAssemblyName}' name in the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.TestAssemblyNameKey}' configuration is not valid.");
                }
            }
            else
            {
                try
                {
                    // Using default dependency context since test assembly is still not loaded.
                    var assemblyName = DependencyContext
                        .Default
                        .GetDefaultAssemblyNames()
                        .First();

                    testAssemblyName = assemblyName.Name;
                    testAssembly = Assembly.Load(assemblyName);
                }
                catch
                {
                    // Intentional silent fail.
                }
            }

            testAssemblyScanned = true;
        }

        private static void TryFindWebAssembly()
        {
            if (webAssembly != null || webAssemblyScanned)
            {
                return;
            }

            var webAssemblyNameFromConfiguration = GeneralConfiguration.WebAssemblyName;
            if (webAssemblyNameFromConfiguration != null)
            {
                try
                {
                    webAssemblyName = webAssemblyNameFromConfiguration;
                    webAssembly = Assembly.Load(new AssemblyName(webAssemblyName));
                }
                catch
                {
                    throw new InvalidOperationException($"Web assembly could not be loaded. The provided '{webAssemblyName}' name in the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.WebAssemblyNameKey}' configuration is not valid.");
                }
            }
            else
            {
                try
                {
                    var testLibrary = ProjectLibraries.First();
                    var dependencies = testLibrary.Dependencies;

                    // Search for a single dependency of the test project which starts with the same namespace.
                    var dependenciesWithSameNamespace = dependencies
                        .Where(d => d.Name.StartsWith(testLibrary.Name.Split('.').First()))
                        .ToList();

                    if (dependenciesWithSameNamespace.Count == 1)
                    {
                        webAssemblyName = dependenciesWithSameNamespace.First().Name;
                        webAssembly = Assembly.Load(webAssemblyName);
                    }
                    else
                    {
                        // Fallback to search for a single dependency of the test project which has an entry Main method.
                        var dependenciesWithEntryPoint = ProjectLibraries
                            .Select(a => a.Name)
                            .Intersect(dependencies.Select(d => d.Name))
                            .Select(l => Assembly.Load(new AssemblyName(l)))
                            .Where(a => a.EntryPoint != null)
                            .ToList();

                        if (dependenciesWithEntryPoint.Count == 1)
                        {
                            webAssemblyName = dependenciesWithEntryPoint.First().GetShortName();
                            webAssembly = Assembly.Load(webAssemblyName);
                        }
                    }
                }
                catch
                {
                    // Intentional silent fail.
                }
            }

            webAssemblyScanned = true;
        }

        private static void EnsureTestAssembly()
        {
            if (TestAssembly == null)
            {
                throw new InvalidOperationException($"Test assembly could not be loaded. You can specify it explicitly in the test configuration ('{DefaultConfigurationFile}' file by default) by providing a value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.TestAssemblyNameKey}' option or set it by calling '.StartsFrom<TStartup>().WithTestAssembly(this)'.");
            }
        }
    }
}
