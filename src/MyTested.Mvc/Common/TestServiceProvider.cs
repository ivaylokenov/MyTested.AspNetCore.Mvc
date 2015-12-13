namespace MyTested.Mvc.Common
{
    using Microsoft.AspNet.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
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
            serviceCollection = new ServiceCollection();
            serviceCollection.AddMvc();

            if (servicesAction != null)
            {
                servicesAction(serviceCollection);
            }
        }

        public static void Setup<TStartup>(Action<IServiceCollection> servicesAction)
            where TStartup : class, new()
        {
            serviceCollection = new ServiceCollection();
            
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

        public static TInstance TryCreateInstance<TInstance>()
            where TInstance : class
        {
            var typeActivatorCache = GetRequiredService<ITypeActivatorCache>();

            try
            {
                return typeActivatorCache.CreateInstance<TInstance>(Current, typeof(TInstance));
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public static void Clear()
        {
            serviceCollection = null;
            serviceProvider = null;
        }
    }
}
