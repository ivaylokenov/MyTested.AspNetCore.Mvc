namespace MyTested.Mvc.Internal
{
    using Caching;
    using Contracts;
    using Logging;
    using Microsoft.AspNet.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using Utilities;
    using Utilities.Validators;

    public class TestServiceProvider
    {
        private const string ConfigureServicesMethodName = "ConfigureServices";

        private static IServiceCollection serviceCollection;
        private static IServiceProvider serviceProvider;

        public static IServiceProvider Current => serviceProvider ?? serviceCollection?.BuildServiceProvider();

        public static void Setup(Action<IServiceCollection> servicesAction)
        {
            serviceCollection = GetInitialServiceCollection();
            serviceCollection.AddMvc();

            if (servicesAction != null)
            {
                servicesAction(serviceCollection);
            }
        }

        public static void Setup<TStartup>(Action<IServiceCollection> servicesAction)
            where TStartup : class, new()
        {
            serviceCollection = GetInitialServiceCollection();

            var configureAction = Reflection.CreateDelegateFromMethod<Action<IServiceCollection>>(
                new TStartup(),
                m => m.Name == ConfigureServicesMethodName && m.ReturnType == typeof(void));

            if (configureAction != null)
            {
                configureAction(serviceCollection);
            }
            else
            {
                var configureFunc = Reflection.CreateDelegateFromMethod<Func<IServiceCollection, IServiceProvider>>(
                    new TStartup(),
                    m => m.Name == ConfigureServicesMethodName && m.ReturnType == typeof(IServiceProvider));

                if (configureFunc != null)
                {
                    configureFunc(serviceCollection);
                }
                else
                {
                    throw new InvalidOperationException($"The provided {typeof(TStartup).Name} class should have method named '{ConfigureServicesMethodName}' with void or {typeof(IServiceProvider).Name} return type.");
                }
            }

            if (servicesAction != null)
            {
                servicesAction(serviceCollection);
            }
        }

        public static bool IsAvailable => Current != null;

        public static TInstance GetRequiredService<TInstance>()
        {
            var service = Current.GetService<TInstance>();
            ServiceValidator.ValidateServiceExists(service);
            return service;
        }

        public static TInstance GetService<TInstance>()
        {
            ServiceValidator.ValidateServices();
            return Current.GetService<TInstance>();
        }

        public static IEnumerable<TInstance> GetServices<TInstance>()
        {
            ServiceValidator.ValidateServices();
            return Current.GetServices<TInstance>();
        }

        public static TInstance TryGetService<TInstance>()
            where TInstance : class
        {
            if (IsAvailable)
            {
                return Current.GetService<TInstance>();
            }

            return null;
        }
        
        public static TInstance TryCreateInstance<TInstance>()
            where TInstance : class
        {
            try
            {
                var typeActivatorCache = GetRequiredService<ITypeActivatorCache>();
                return typeActivatorCache.CreateInstance<TInstance>(Current, typeof(TInstance));
            }
            catch
            {
                return null;
            }
        }

        public static void Clear()
        {
            serviceCollection = null;
            serviceProvider = null;
        }

        private static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<ILoggerFactory>(MockedLoggerFactory.Create());
            serviceCollection.TryAddSingleton<IControllerActionDescriptorCache, ControllerActionDescriptorCache>();
            return serviceCollection;
        }
    }
}
