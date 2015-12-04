namespace MyTested.Mvc.Common
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Utilities;

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

        public static void Clear()
        {
            serviceCollection = null;
            serviceProvider = null;
        }
    }
}
