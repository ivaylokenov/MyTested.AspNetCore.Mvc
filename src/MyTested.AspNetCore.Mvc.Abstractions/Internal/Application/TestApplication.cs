namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Configuration;
    using Licensing;
    using Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Builder;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Http;
#if NET451
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
#endif
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.DotNet.InternalAbstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.DependencyModel;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.PlatformAbstractions;
    using Plugins;
    using Services;
    using Utilities.Extensions;

    public static class TestApplication
    {
        private const string TestFrameworkName = "MyTested.AspNetCore.Mvc";
        private const string ReleaseDate = "2016-10-01";

        private static readonly RequestDelegate NullHandler;

        private static readonly ISet<IDefaultRegistrationPlugin> DefaultRegistrationPlugins;
        private static readonly ISet<IServiceRegistrationPlugin> ServiceRegistrationPlugins;
        private static readonly ISet<IRoutingServiceRegistrationPlugin> RoutingServiceRegistrationPlugins;
        private static readonly ISet<IInitializationPlugin> InitializationPlugins;

        private static readonly object Sync;

        private static bool initialiazed;

        private static string testAssemblyName;
        private static TestConfiguration testConfiguration;

        private static IHostingEnvironment environment;

        private static Type startupType;
        private static string startupAssemblyName;

        private static volatile IServiceProvider serviceProvider;
        private static volatile IServiceProvider routingServiceProvider;
        private static volatile IRouter router;

        static TestApplication()
        {
            NullHandler = c => TaskCache.CompletedTask;
            Sync = new object();

            DefaultRegistrationPlugins = new HashSet<IDefaultRegistrationPlugin>();
            ServiceRegistrationPlugins = new HashSet<IServiceRegistrationPlugin>();
            RoutingServiceRegistrationPlugins = new HashSet<IRoutingServiceRegistrationPlugin>();
            InitializationPlugins = new HashSet<IInitializationPlugin>();

#if NET451
            FindTestAssembly();
#endif

            PrepareTestConfiguration();
            FindTestAssemblyName();
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

        internal static Assembly TestAssembly { get; set; }

        internal static Type StartupType
        {
            get
            {
                return startupType;
            }

            set
            {
                if (startupType != null && TestConfiguration.General.AsynchronousTests)
                {
                    throw new InvalidOperationException("Multiple Startup types per test project while running asynchronous tests is not supported. Either set 'General.AsynchronousTests' in the 'testconfig.json' file to 'false' or separate your tests into different test projects. The latter is recommended. If you choose the first option, you may need to disable asynchronous testing in your preferred test runner too.");
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

        internal static string StartupAssemblyName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(startupAssemblyName))
                {
                    startupAssemblyName = StartupType?.GetTypeInfo().Assembly.GetName().Name;
                }

                return startupAssemblyName;
            }
        }

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

        public static TestConfiguration TestConfiguration
        {
            get
            {
                if (testConfiguration == null || AdditionalConfiguration != null)
                {
                    testConfiguration = TestConfiguration.With(PrepareTestConfiguration());
                    PrepareLicensing();
                }

                return testConfiguration;
            }
        }

        internal static string ApplicationName =>
            TestConfiguration.General.ApplicationName
                ?? TestAssembly?.GetName().Name
                ?? StartupAssemblyName
                ?? PlatformServices.Default.Application.ApplicationName;

        public static void TryInitialize()
        {
            lock (Sync)
            {
                if (!initialiazed && TestConfiguration.General.AutomaticStartup)
                {
                    var defaultStartupType = TryFindDefaultStartupType();

                    if (defaultStartupType == null)
                    {
                        throw new InvalidOperationException($"{Environment.EnvironmentName}Startup class could not be found at the root of the test project. Either add it or set 'General.AutomaticStartup' in the 'testconfig.json' file to 'false'.");
                    }

                    startupType = defaultStartupType;
                    Initialize();
                }
            }
        }

        internal static DependencyContext LoadDependencyContext()
            => TestAssembly != null
            ? DependencyContext.Load(TestAssembly)
            ?? DependencyContext.Default
            : DependencyContext.Default;

        internal static void LoadPlugins(DependencyContext dependencyContext)
        {
            var plugins = dependencyContext
                .GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier())
                .Where(l => l.Name.StartsWith(TestFrameworkName))
                .Select(l => Assembly.Load(new AssemblyName(l.Name)).GetType($"{TestFrameworkName}.Plugins.{l.Name.Replace(TestFrameworkName, string.Empty).Trim('.')}TestPlugin"))
                .Where(p => p != null);

            if (!plugins.Any())
            {
                throw new InvalidOperationException("Test plugins could not be loaded. Depending on your project's configuration you may need to set the 'preserveCompilationContext' property under 'buildOptions' to 'true' in the test assembly's 'project.json' file and/or may need to call '.StartsFrom<TStartup>().WithTestAssembly(this)'.");
            }

            plugins.ForEach(t =>
            {
                var plugin = Activator.CreateInstance(t);

                var defaultRegistrationPlugin = plugin as IDefaultRegistrationPlugin;
                if (defaultRegistrationPlugin != null)
                {
                    DefaultRegistrationPlugins.Add(defaultRegistrationPlugin);
                }

                var servicePlugin = plugin as IServiceRegistrationPlugin;
                if (servicePlugin != null)
                {
                    ServiceRegistrationPlugins.Add(servicePlugin);
                }

                var routingServicePlugin = plugin as IRoutingServiceRegistrationPlugin;
                if (routingServicePlugin != null)
                {
                    RoutingServiceRegistrationPlugins.Add(routingServicePlugin);
                }

                var initializationPlugin = plugin as IInitializationPlugin;
                if (initializationPlugin != null)
                {
                    InitializationPlugins.Add(initializationPlugin);
                }

                var httpFeatureRegistrationPlugin = plugin as IHttpFeatureRegistrationPlugin;
                if (httpFeatureRegistrationPlugin != null)
                {
                    TestHelper.HttpFeatureRegistrationPlugins.Add(httpFeatureRegistrationPlugin);
                }

                var shouldPassForPlugin = plugin as IShouldPassForPlugin;
                if (shouldPassForPlugin != null)
                {
                    TestHelper.ShouldPassForPlugins.Add(shouldPassForPlugin);
                }
            });
        }

        internal static Type TryFindDefaultStartupType()
        {
            var applicationAssembly = TestAssembly ?? Assembly.Load(new AssemblyName(testAssemblyName));

            var defaultStartupType = TestConfiguration.General.StartupType ?? $"{Environment.EnvironmentName}Startup";

            // check root of the test project
            var startup =
                applicationAssembly.GetType(defaultStartupType) ??
                applicationAssembly.GetType($"{applicationAssembly.GetName().Name}.{defaultStartupType}");

            return startup;
        }

        private static void Initialize()
        {
            PrepareLicensing();

            var dependencyContext = LoadDependencyContext();
            LoadPlugins(dependencyContext);

            var serviceCollection = GetInitialServiceCollection();
            var startupMethods = PrepareStartup(serviceCollection);

            PrepareServices(serviceCollection, startupMethods);
            PrepareApplicationAndRouting(startupMethods);

            initialiazed = true;
        }

        private static IConfiguration PrepareTestConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("testconfig.json", optional: true);

            AdditionalConfiguration?.Invoke(configurationBuilder);
            AdditionalConfiguration = null;

            return configurationBuilder.Build();
        }

        private static void FindTestAssemblyName()
        {
            testAssemblyName = TestConfiguration.General.TestAssemblyName
                ?? TestAssembly?.GetName().Name
                ?? DependencyContext
                    .Default
                    .GetDefaultAssemblyNames()
                    .First()
                    .Name;
        }

        private static void PrepareLicensing()
        {
            TestCounter.SetLicenseData(
                TestConfiguration.Licenses,
                DateTime.ParseExact(ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                TestAssembly?.GetName().Name ?? StartupAssemblyName);
        }

        private static IHostingEnvironment PrepareEnvironment()
        {
            return new HostingEnvironment
            {
                ApplicationName = ApplicationName,
                EnvironmentName = TestConfiguration.General.EnvironmentName,
                ContentRootPath = PlatformServices.Default.Application.ApplicationBasePath
            };
        }

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var diagnosticSource = new DiagnosticListener(TestFrameworkName);

            // default server services
            serviceCollection.TryAddSingleton(Environment);

            serviceCollection.TryAddSingleton<ILoggerFactory>(LoggerFactoryMock.Create());
            serviceCollection.AddLogging();

            serviceCollection.AddTransient<IApplicationBuilderFactory, ApplicationBuilderFactory>();
            serviceCollection.TryAddTransient<IHttpContextFactory, HttpContextFactory>();
            serviceCollection.AddOptions();

            serviceCollection.TryAddSingleton<DiagnosticSource>(diagnosticSource);
            serviceCollection.TryAddSingleton(diagnosticSource);

            serviceCollection.AddTransient<IStartupFilter, AutoRequestServicesStartupFilter>();

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
                startupMethods.ConfigureServicesDelegate(serviceCollection);
            }
            else
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

            AdditionalServices?.Invoke(serviceCollection);

            TryReplaceKnownServices(serviceCollection);
            PrepareRoutingServices(serviceCollection);

#if NET451
            var baseStartupType = StartupType;
            while (baseStartupType?.BaseType != typeof(object))
            {
                baseStartupType = baseStartupType.BaseType;
            }

            var applicationPartManager = (ApplicationPartManager)serviceCollection
                .FirstOrDefault(t => t.ServiceType == typeof(ApplicationPartManager))
                ?.ImplementationInstance;

            if (applicationPartManager != null && baseStartupType != null)
            {
                applicationPartManager.ApplicationParts.Add(new AssemblyPart(baseStartupType.GetTypeInfo().Assembly));
            }
#endif

            serviceProvider = serviceCollection.BuildServiceProvider();

            InitializationPlugins.ForEach(plugin => plugin.InitializationDelegate(serviceProvider));
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

        private static void PrepareApplicationAndRouting(StartupMethods startupMethods)
        {
            var applicationBuilder = new ApplicationBuilderMock(serviceProvider);

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
            environment = null;
            startupType = null;
            serviceProvider = null;
            routingServiceProvider = null;
            router = null;
            AdditionalServices = null;
            AdditionalApplicationConfiguration = null;
            AdditionalRouting = null;
            TestAssembly = null;
            TestServiceProvider.Current = null;
            TestServiceProvider.ClearServiceLifetimes();
            DefaultRegistrationPlugins.Clear();
            ServiceRegistrationPlugins.Clear();
            RoutingServiceRegistrationPlugins.Clear();
            InitializationPlugins.Clear();
            LicenseValidator.ClearLicenseDetails();
    }

#if NET451
    private static void FindTestAssembly()
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        var stackTrace = new StackTrace(false);

        foreach (var frame in stackTrace.GetFrames())
        {
            var method = frame.GetMethod();
            var methodAssembly = method?.DeclaringType?.Assembly;

            if (methodAssembly != null
                && methodAssembly != executingAssembly
                && !methodAssembly.FullName.StartsWith(TestFrameworkName))
            {
                TestAssembly = methodAssembly;
                return;
            }
        }
    }
#endif
}
}
