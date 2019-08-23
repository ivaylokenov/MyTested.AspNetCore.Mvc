namespace MyTested.AspNetCore.Mvc.Internal.Services
{
    using System;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Routing;

    public static class ServiceCollectionInternalExtensions
    {
        public static IServiceCollection Clone(this IServiceCollection serviceCollection)
            => new ServiceCollection { serviceCollection };

        public static IRoutingServices AddRoutingServices(this IServiceCollection serviceCollection)
        {
            var routingServices = new RoutingServices
            {
                ServiceCollection = serviceCollection
            };

            serviceCollection.AddSingleton<IRoutingServices>(routingServices);

            return routingServices;
        }

        public static IServiceCollection AddModelBindingActionInvoker(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddEnumerable(
                ServiceDescriptor.Transient<IActionInvokerProvider, ModelBindingActionInvokerProvider>());

            serviceCollection.TryAddSingleton<ModelBindingActionInvokerCache>();
            serviceCollection.TryAddSingleton<IModelBindingActionInvokerFactory, ModelBindingActionInvokerFactory>();
            
            return serviceCollection;
        }

        public static IServiceProvider BuildServiceProviderFromFactory(this IServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();
            var factory = provider.GetService<IServiceProviderFactory<IServiceCollection>>();

            if (factory != null && !(factory is DefaultServiceProviderFactory))
            {
                using (provider)
                {
                    return factory.CreateServiceProvider(factory.CreateBuilder(serviceCollection));
                }
            }

            return provider;
        }
    }
}
