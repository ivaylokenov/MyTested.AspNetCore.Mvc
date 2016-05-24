namespace MyTested.Mvc.Internal.Application
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Caching;
    using Contracts;
    using Controllers;
    using Formatters;
    using Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Hosting.Startup;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.DependencyModel;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.PlatformAbstractions;
    using Routes;
    using Utilities.Extensions;

    public static class TestApplication
    {
        private const string TestFrameworkName = "MyTested.Mvc";

        private static readonly RequestDelegate NullHandler = c => TaskCache.CompletedTask;
        private static readonly object Sync;

        private static bool initialiazed;

        private static TestConfiguration testConfiguration;
        private static string testAssemblyName;

        private static IConfiguration configuration;
        private static IHostingEnvironment environment;

        private static Type startupType;

        private static volatile IServiceProvider serviceProvider;
        private static volatile IServiceProvider routeServiceProvider;
        private static volatile IRouter router;

        static TestApplication()
        {
            Sync = new object();
            configuration = PrepareConfiguration();
        }

        public static IServiceProvider Services
        {
            get
            {
                TryLockedInitialization();
                return serviceProvider;
            }
        }

        public static IServiceProvider RouteServices
        {
            get
            {
                TryLockedInitialization();
                return routeServiceProvider;
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

        internal static Type StartupType
        {
            get
            {
                return startupType;
            }

            set
            {
                Reset();
                startupType = value;
            }
        }

        internal static Action<IConfigurationBuilder> AdditionalConfiguration { get; set; }

        internal static Action<IServiceCollection> AdditionalServices { get; set; }

        internal static Action<IApplicationBuilder> AdditionalApplicationConfiguration { get; set; }

        internal static Action<IRouteBuilder> AdditionalRoutes { get; set; }

        internal static string TestAssemblyName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(testAssemblyName))
                {
                    var startUpAssemblyFullName = StartupType?.GetTypeInfo().Assembly.FullName;
                    if (startUpAssemblyFullName != null)
                    {
                        testAssemblyName = new AssemblyName(testAssemblyName).Name;
                    }
                }

                return testAssemblyName;
            }
        }

        internal static IConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = PrepareConfiguration();
                }

                return configuration;
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

        internal static TestConfiguration TestConfiguration
        {
            get
            {
                if (testConfiguration == null)
                {
                    testConfiguration = TestConfiguration.With(configuration);
                }

                return testConfiguration;
            }
        }

        internal static string ApplicationName => 
            TestConfiguration.ApplicationName
                ?? TestAssemblyName
                ?? PlatformServices.Default.Application.ApplicationName;

        internal static void TryInitialize()
        {
            if (TestConfiguration.AutomaticStartup)
            {
                startupType = TryFindDefaultStartupType();

                if (startupType != null)
                {
                    Initialize();
                }
            }
        }

        internal static Type TryFindDefaultStartupType()
        {
            testAssemblyName = DependencyContext.Default
                .RuntimeLibraries
                .Where(l => l.Dependencies.Any(d => d.Name.StartsWith(TestFrameworkName)))
                .Select(l => l.Name)
                .First();

            var applicationAssembly = Assembly.Load(new AssemblyName(testAssemblyName));

            var startupName = TestConfiguration.FullStartupName ?? $"{Environment.EnvironmentName}Startup";

            // check root of the test project
            var startup =
                applicationAssembly.GetType(startupName) ??
                applicationAssembly.GetType($"{testAssemblyName}.{startupName}");

            return startup;
        }

        private static void Initialize()
        {
            var serviceCollection = GetInitialServiceCollection();

            StartupMethods startupMethods = null;
            if (StartupType != null)
            {
                startupMethods = serviceCollection
                    .BuildServiceProvider()
                    .GetRequiredService<IStartupLoader>()
                    .LoadMethods(StartupType, null);
            }

            PrepareServices(serviceCollection, startupMethods);
            PrepareApplicationAndRoutes(startupMethods);

            initialiazed = true;
        }

        private static IConfiguration PrepareConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("testconfig.json", optional: true);

            AdditionalConfiguration?.Invoke(configurationBuilder);

            return configurationBuilder.Build();
        }

        private static IHostingEnvironment PrepareEnvironment()
        {
            return new HostingEnvironment
            {
                ApplicationName = ApplicationName,
                EnvironmentName = TestConfiguration.EnvironmentName,
                ContentRootPath = PlatformServices.Default.Application.ApplicationBasePath
            };
        }

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var diagnosticSource = new DiagnosticListener(TestFrameworkName);

            // default server services
            serviceCollection.TryAddSingleton(Environment);
            serviceCollection.TryAddSingleton<ILoggerFactory>(MockedLoggerFactory.Create());

            serviceCollection.AddTransient<IStartupLoader, StartupLoader>();

            serviceCollection.TryAddTransient<IHttpContextFactory, HttpContextFactory>();
            serviceCollection.AddLogging();
            serviceCollection.AddOptions();

            serviceCollection.TryAddSingleton<DiagnosticSource>(diagnosticSource);
            serviceCollection.TryAddSingleton(diagnosticSource);

            serviceCollection.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            // platform services
            AddPlatformServices(serviceCollection);

            // testing framework services
            serviceCollection.TryAddSingleton<IValidControllersCache, ValidControllersCache>();
            serviceCollection.TryAddSingleton<IControllerActionDescriptorCache, ControllerActionDescriptorCache>();

            return serviceCollection;
        }

        private static void PrepareServices(IServiceCollection serviceCollection, StartupMethods startupMethods)
        {
            if (startupMethods?.ConfigureServicesDelegate != null)
            {
                startupMethods.ConfigureServicesDelegate(serviceCollection);
            }
            else
            {
                serviceCollection.AddMvc();
            }

            AdditionalServices?.Invoke(serviceCollection);

            // custom MVC options
            serviceCollection.Configure<MvcOptions>(options =>
            {
                // add controller conventions to save all valid controller types
                options.Conventions.Add(new ValidControllersCache());

                // string input formatter helps with HTTP request processing
                var inputFormatters = options.InputFormatters.OfType<TextInputFormatter>();
                if (!inputFormatters.Any(f => f.SupportedMediaTypes.Contains(ContentType.TextPlain)))
                {
                    options.InputFormatters.Add(new StringInputFormatter());
                }
            });

            TryReplaceCommonServices(serviceCollection);
            PrepareRouteServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            // this call prepares all application conventions and fills the controller action descriptor cache
            serviceProvider.GetService<IControllerActionDescriptorCache>();
        }

        private static void TryReplaceCommonServices(IServiceCollection serviceCollection)
        {
            var tempDataProviderServiceType = typeof(ITempDataProvider);
            var defaultTempDataProviderType = typeof(SessionStateTempDataProvider);
            var memoryCacheServiceType = typeof(IMemoryCache);
            var defaultMemoryCacheType = typeof(MemoryCache);
            var sessionStoreServiceType = typeof(ISessionStore);
            var defaultSessionStoreType = typeof(DistributedSessionStore);

            var setMockedTempData = false;
            var setMockedCaching = false;
            var setMockedSession = false;

            serviceCollection.ForEach(service =>
            {
                var serviceType = service.ServiceType;
                var implementationType = service.ImplementationType;

                TestServiceProvider.SaveServiceLifetime(serviceType, service.Lifetime);

                if (serviceType == tempDataProviderServiceType)
                {
                    if (implementationType == defaultTempDataProviderType)
                    {
                        setMockedTempData = true;
                    }
                    else
                    {
                        setMockedTempData = false;
                    }
                }

                if (serviceType == memoryCacheServiceType)
                {
                    if (implementationType == defaultMemoryCacheType)
                    {
                        setMockedCaching = true;
                    }
                    else
                    {
                        setMockedCaching = false;
                    }
                }

                if (serviceType == sessionStoreServiceType)
                {
                    if (implementationType == defaultSessionStoreType)
                    {
                        setMockedSession = true;
                    }
                    else
                    {
                        setMockedSession = false;
                    }
                }
            });

            if (setMockedTempData)
            {
                serviceCollection.ReplaceTempDataProvider();
            }

            if (setMockedCaching)
            {
                serviceCollection.ReplaceMemoryCache();
                TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
            }

            if (setMockedSession)
            {
                serviceCollection.ReplaceSession();
            }
        }

        private static void PrepareRouteServices(IServiceCollection serviceCollection)
        {
            var modelBindingActionInvokerFactoryType = typeof(IModelBindingActionInvokerFactory);

            if (serviceCollection.All(s => s.ServiceType != modelBindingActionInvokerFactoryType))
            {
                serviceCollection.TryAddEnumerable(
                    ServiceDescriptor.Transient<IActionInvokerProvider, ModelBindingActionInvokerProvider>());
                serviceCollection.TryAddSingleton(modelBindingActionInvokerFactoryType, typeof(ModelBindingActionInvokerFactory));
            }

            routeServiceProvider = serviceCollection.BuildServiceProvider();

            serviceCollection.TryRemoveSingleton(modelBindingActionInvokerFactoryType);

            var actionInvokerProviders = serviceCollection.Where(s => s.ServiceType == typeof(IActionInvokerProvider)).ToList();
            if (actionInvokerProviders.Count > 1)
            {
                serviceCollection.Remove(actionInvokerProviders.LastOrDefault());
            }
        }

        private static void PrepareApplicationAndRoutes(StartupMethods startupMethods)
        {
            var applicationBuilder = new MockedApplicationBuilder(serviceProvider);

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

            AdditionalRoutes?.Invoke(routeBuilder);

            if (StartupType == null || routeBuilder.Routes.Count == 0)
            {
                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routeBuilder.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(
                    routeBuilder.DefaultHandler,
                    serviceProvider));
            }

            router = routeBuilder.Build();
        }

        private static void AddPlatformServices(IServiceCollection serviceCollection)
        {
            var defaultPlatformServices = PlatformServices.Default;
            if (defaultPlatformServices != null)
            {
                if (defaultPlatformServices.Application != null)
                {
                    var appEnv = defaultPlatformServices.Application;
                    serviceCollection.TryAddSingleton(appEnv);
                }

                if (defaultPlatformServices.Runtime != null)
                {
                    serviceCollection.TryAddSingleton(defaultPlatformServices.Runtime);
                }
            }
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
            configuration = null;
            environment = null;
            startupType = null;
            serviceProvider = null;
            routeServiceProvider = null;
            router = null;
            AdditionalServices = null;
            AdditionalApplicationConfiguration = null;
            AdditionalRoutes = null;
            TestServiceProvider.Current = null;
            TestServiceProvider.ClearServiceLifetimes();
        }
    }
}
