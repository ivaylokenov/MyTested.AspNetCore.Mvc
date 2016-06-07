namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Caching;
    using Contracts;
    using Controllers;
    using Formatters;
    using Licensing;
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
    using Microsoft.AspNetCore.Routing;
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
        private const string TestFrameworkName = "MyTested.AspNetCore.Mvc";

        private static readonly RequestDelegate NullHandler = c => TaskCache.CompletedTask;
        private static readonly ISet<IDefaultRegistrationPlugin> DefaultRegistrationPlugins = new HashSet<IDefaultRegistrationPlugin>();
        private static readonly ISet<IServiceRegistrationPlugin> ServiceRegistrationPlugins = new HashSet<IServiceRegistrationPlugin>();
        private static readonly object Sync;

        private static bool initialiazed;

        private static TestConfiguration testConfiguration;
        private static string testAssemblyName;
        
        private static IHostingEnvironment environment;

        private static Type startupType;

        private static volatile IServiceProvider serviceProvider;
        private static volatile IServiceProvider routeServiceProvider;
        private static volatile IRouter router;

        static TestApplication()
        {
            Sync = new object();
            LoadPlugins();
            PrepareTestConfiguration();
            FindTestAssemblyName();
            PrepareLicensing();
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
                if (testConfiguration == null || AdditionalConfiguration != null)
                {
                    testConfiguration = TestConfiguration.With(PrepareTestConfiguration());
                    PrepareLicensing();
                }

                return testConfiguration;
            }
        }

        internal static string ApplicationName =>
            TestConfiguration.ApplicationName
                ?? TestAssemblyName
                ?? PlatformServices.Default.Application.ApplicationName;

        internal static void LoadPlugins()
        {
            DependencyContext.Default
                .RuntimeLibraries
                .Where(l => l.Name.StartsWith(TestFrameworkName))
                .Select(l => Assembly.Load(new AssemblyName(l.Name)).GetType($"{l.Name}.{l.Name.Replace(TestFrameworkName, string.Empty).Trim('.')}TestPlugin"))
                .Where(p => p != null)
                .ForEach(t =>
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

                    var httpFeatureRegistrationPlugin = plugin as IHttpFeatureRegistrationPlugin;
                    if (httpFeatureRegistrationPlugin != null)
                    {
                        TestHelper.HttpFeatureRegistrationPlugins.Add(httpFeatureRegistrationPlugin);
                    }
                });
        }

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
            testAssemblyName = DependencyContext.Default
                .RuntimeLibraries
                .Where(l => l.Dependencies.Any(d => d.Name.StartsWith(TestFrameworkName)))
                .Select(l => l.Name)
                .First();
        }

        private static void PrepareLicensing()
        {
            TestCounter.SetLicenseData(
                TestConfiguration.Licenses,
                DateTime.ParseExact(MyMvc.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                TestAssemblyName);
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
                var defaultRegistrationPlugin = DefaultRegistrationPlugins
                    .OrderByDescending(p => p.Priority)
                    .FirstOrDefault();

                if (defaultRegistrationPlugin != null)
                {
                    defaultRegistrationPlugin.DefaultServiceRegistrationDelegate(serviceCollection);
                }
                else
                {
                    serviceCollection
                        .AddMvcCore()
                        .AddFormatterMappings()
                        .AddJsonFormatters();
                }
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

            TryReplaceKnownServices(serviceCollection);
            PrepareRouteServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            // this call prepares all application conventions and fills the controller action descriptor cache
            serviceProvider.GetService<IControllerActionDescriptorCache>();
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

            serviceCollection.RemoveSingleton(modelBindingActionInvokerFactoryType);

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
