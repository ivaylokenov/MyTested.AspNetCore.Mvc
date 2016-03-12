namespace MyTested.Mvc.Internal.Application
{
    using System;
    using Caching;
    using Contracts;
    using Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Hosting.Startup;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Routes;
    using System.Linq;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Formatters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.PlatformAbstractions;
    using System.Reflection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Controllers;

    public static class TestApplication
    {
        private static readonly RequestDelegate NullHandler = (c) => TaskCache.CompletedTask;

        private static bool initialiazed;
        private static object sync;

        private static TestConfiguration testConfiguration;

        private static IConfiguration configuration;
        private static IHostingEnvironment environment;

        private static Type startupType;

        private static volatile IServiceProvider serviceProvider;
        private static volatile IServiceProvider routeServiceProvider;
        private static volatile IRouter router;

        static TestApplication()
        {
            sync = new object();
            configuration = PrepareConfiguration();
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
            var applicationName = PlatformServices.Default.Application.ApplicationName;
            var applicationAssembly = Assembly.Load(new AssemblyName(applicationName));

            var startupName = TestConfiguration.FullStartupName ?? $"{Environment.EnvironmentName}Startup";

            // check root of the testing library
            var startup =
                applicationAssembly.GetType(startupName) ??
                applicationAssembly.GetType($"{applicationName}.{startupName}");

            if (startup == null)
            {
                // full scan 
                var startupTypes = applicationAssembly
                    .DefinedTypes
                    .Where(t =>
                    {
                        return t.Name == startupName || t.Name == $"{applicationName}.{startupName}";
                    })
                    .Select(t => t.AsType())
                    .ToArray();

                if (startupTypes.Length == 1)
                {
                    startup = startupTypes.First();
                }
            }

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

            if (AdditionalConfiguration != null)
            {
                AdditionalConfiguration(configurationBuilder);
            }

            return configurationBuilder.Build();
        }

        private static IHostingEnvironment PrepareEnvironment()
        {
            return new HostingEnvironment
            {
                Configuration = Configuration,
                EnvironmentName = TestConfiguration.EnvironmentName
            };
        }

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var diagnosticSource = new DiagnosticListener("MyTested.Mvc");

            // default server services
            serviceCollection.TryAddSingleton(Environment);
            serviceCollection.TryAddSingleton<ILoggerFactory>(MockedLoggerFactory.Create());

            serviceCollection.AddTransient<IStartupLoader, StartupLoader>();

            serviceCollection.TryAddTransient<IHttpContextFactory, HttpContextFactory>();
            serviceCollection.AddLogging();
            serviceCollection.AddOptions();

            serviceCollection.TryAddSingleton<DiagnosticSource>(diagnosticSource);
            serviceCollection.TryAddSingleton(diagnosticSource);

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

            if (AdditionalServices != null)
            {
                AdditionalServices(serviceCollection);
            }

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

            TryAddControllersAsServices(serviceCollection);
            PrepareRouteServices(serviceCollection);

            serviceCollection.TryReplaceSingleton<ITempDataProvider, MockedTempDataProvider>();

            if (serviceCollection.Any(s => s.ServiceType == typeof(IMemoryCache)))
            {
                serviceCollection.TryReplace<IMemoryCache, MockedMemoryCache>(ServiceLifetime.Transient);
            }

            serviceProvider = serviceCollection.BuildServiceProvider();

            // this call prepares all application conventions and fills the controller action descriptor cache
            serviceProvider.GetService<IControllerActionDescriptorCache>();
        }

        private static void TryAddControllersAsServices(IServiceCollection serviceCollection)
        {
            if (StartupType != null)
            {
                var startupTypeInfo = StartupType.GetTypeInfo();

                while (startupTypeInfo.BaseType != null && startupTypeInfo.BaseType != typeof(object))
                {
                    startupTypeInfo = startupTypeInfo.BaseType.GetTypeInfo();
                }

                if (startupTypeInfo.Assembly.GetName().Name != PlatformServices.Default.Application.ApplicationName)
                {
                    var controllerTypeProvider = serviceCollection
                        .Where(s => s.ServiceType == typeof(IControllerTypeProvider))
                        .Select(s => s.ImplementationInstance)
                        .OfType<StaticControllerTypeProvider>()
                        .FirstOrDefault();

                    if (controllerTypeProvider == null || !controllerTypeProvider.ControllerTypes.Any())
                    {
                        serviceCollection.AddMvcControllersAsServices(startupTypeInfo.Assembly);
                    }
                }
            }
        }

        private static void PrepareRouteServices(IServiceCollection serviceCollection)
        {
            var modelBindingActionInvokerFactoryType = typeof(IModelBindingActionInvokerFactory);

            if (!serviceCollection.Any(s => s.ServiceType == modelBindingActionInvokerFactoryType))
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

            if (startupMethods?.ConfigureDelegate != null)
            {
                startupMethods.ConfigureDelegate(applicationBuilder);
            }

            if (AdditionalApplicationConfiguration != null)
            {
                AdditionalApplicationConfiguration(applicationBuilder);
            }

            var routeBuilder = new RouteBuilder(applicationBuilder)
            {
                DefaultHandler = new RouteHandler(NullHandler)
            };

            for (int i = 0; i < applicationBuilder.Routes.Count; i++)
            {
                var route = applicationBuilder.Routes[i];
                routeBuilder.Routes.Add(route);
            }

            if (AdditionalRoutes != null)
            {
                AdditionalRoutes(routeBuilder);
            }

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
                lock (sync)
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
        }
    }
}
