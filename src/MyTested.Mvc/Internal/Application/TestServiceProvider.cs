namespace MyTested.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    /// <summary>
    /// Provides global application services.
    /// </summary>
    public class TestServiceProvider
    {
        private static readonly TestLocal<IServiceProvider> CurrentServiceProvider
            = new TestLocal<IServiceProvider>();

        private static readonly IDictionary<Type, ServiceLifetime> ServiceLifetimes
            = new Dictionary<Type, ServiceLifetime>();

        /// <summary>
        /// Gets the global service provider.
        /// </summary>
        /// <value>Type of IServiceProvider.</value>
        public static IServiceProvider Global => TestApplication.Services;
        
        public static IServiceProvider Current
        {
            get
            {
                return CurrentServiceProvider.Value ?? Global;
            }

            internal set
            {
                CurrentServiceProvider.Value = value;
            }
        }

        /// <summary>
        /// Gets required service. Throws exception if such is not found or there are no registered services.
        /// </summary>
        /// <typeparam name="TInstance">Type of requested service.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static TInstance GetRequiredService<TInstance>()
        {
            ServiceValidator.ValidateServices();
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
            => Current.GetService<TInstance>();

        public static void SaveServiceLifetime(Type serviceType, ServiceLifetime lifetime)
            => ServiceLifetimes[serviceType] = lifetime;

        public static ServiceLifetime GetServiceLifetime(Type serviceType)
            => ServiceLifetimes.ContainsKey(serviceType)
                ? ServiceLifetimes[serviceType]
                : ServiceLifetime.Transient;

        public static ServiceLifetime GetServiceLifetime<TService>()
            => GetServiceLifetime(typeof(TService));

        internal static void ClearServiceLifetimes() => ServiceLifetimes.Clear();
    }
}
