namespace MyTested.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using Caching;
    using Contracts;
    using Logging;
    using Microsoft.AspNet.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Provides global application services.
    /// </summary>
    public class TestServiceProvider
    {
        private const string ConfigureServicesMethodName = "ConfigureServices";

        private static IServiceCollection serviceCollection;
        private static IServiceProvider serviceProvider;

        /// <summary>
        /// Gets the current service provider.
        /// </summary>
        /// <value>Type of IServiceProvider.</value>
        public static IServiceProvider Current => serviceProvider ?? serviceCollection?.BuildServiceProvider();

        /// <summary>
        /// Gets whether the global services are available.
        /// </summary>
        /// <value>True of False.</value>
        public static bool IsAvailable => Current != null;

        /// <summary>
        /// Setups application services. Initially adds all MVC default services then runs the provided action.
        /// </summary>
        /// <param name="servicesAction">Services action used to register custom application services.</param>
        public static void Setup(Action<IServiceCollection> servicesAction)
        {
            serviceCollection = GetInitialServiceCollection();
            serviceCollection.AddMvc();

            if (servicesAction != null)
            {
                servicesAction(serviceCollection);
            }
        }

        /// <summary>
        /// Setups application services with the provided Startup class and then runs the provided action.
        /// </summary>
        /// <typeparam name="TStartup">Startup class of the tested web application.</typeparam>
        /// <param name="servicesAction">Services action used to register custom application services.</param>
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

        /// <summary>
        /// Gets required service. Throws exception if such is not found or there are no registered services.
        /// </summary>
        /// <typeparam name="TInstance">Type of requested service.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static TInstance GetRequiredService<TInstance>()
        {
            var service = Current.GetService<TInstance>();
            ServiceValidator.ValidateServiceExists(service);
            return service;
        }

        /// <summary>
        /// Gets service. Returns null if such is not found. Throws exception if there are no registered services.
        /// </summary>
        /// <typeparam name="TInstance">Type of requested service.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static TInstance GetService<TInstance>()
        {
            ServiceValidator.ValidateServices();
            return Current.GetService<TInstance>();
        }

        /// <summary>
        /// Gets collection of services. Returns null if no service of this type is not found. Throws exception if there are no registered services.
        /// </summary>
        /// <typeparam name="TInstance">Type of requested service.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static IEnumerable<TInstance> GetServices<TInstance>()
        {
            ServiceValidator.ValidateServices();
            return Current.GetServices<TInstance>();
        }

        /// <summary>
        /// Tries to get service. Returns null if such is not found or no services are registered.
        /// </summary>
        /// <typeparam name="TInstance">Type of requested service.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static TInstance TryGetService<TInstance>()
            where TInstance : class
        {
            if (IsAvailable)
            {
                return Current.GetService<TInstance>();
            }

            return null;
        }

        /// <summary>
        /// Tries to create instance of the provided type. Returns null if not successful.
        /// </summary>
        /// <typeparam name="TInstance">Type to create.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
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

        /// <summary>
        /// Clears the global service collection.
        /// </summary>
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
