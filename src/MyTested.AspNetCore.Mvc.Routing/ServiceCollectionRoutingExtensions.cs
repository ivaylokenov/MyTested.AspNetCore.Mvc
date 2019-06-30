namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Internal;
    using Internal.Contracts;
    using Internal.Routing;
    using Internal.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    /// <summary>
    /// Contains routing extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionRoutingExtensions
    {
        /// <summary>
        /// Adds routing testing services.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRoutingTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            var modelBindingActionInvokerFactoryServiceType = typeof(IModelBindingActionInvokerFactory);
            var routingServicesType = typeof(IRoutingServices);

            IRoutingServices routingServices = null;

            var foundModelBindingActionInvokerFactory = false;

            foreach (var serviceDescriptor in serviceCollection)
            {
                if (serviceDescriptor.ServiceType == routingServicesType)
                {
                    routingServices = serviceDescriptor.ImplementationInstance as IRoutingServices;
                }

                if (serviceDescriptor.ServiceType == modelBindingActionInvokerFactoryServiceType)
                {
                    foundModelBindingActionInvokerFactory = true;
                }
            }

            if (routingServices == null)
            {
                routingServices = serviceCollection.AddRoutingServices();
            }

            var routingServiceCollection = serviceCollection.Clone();

            if (!foundModelBindingActionInvokerFactory)
            {
                routingServiceCollection.AddModelBindingActionInvoker();
            }

            routingServices.ServiceCollection = routingServiceCollection;

            return serviceCollection;
        }

        /// <summary>
        /// Adds routing testing services.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> to use for the routing testing.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRoutingTesting(
            this IServiceCollection serviceCollection,
            IServiceProvider serviceProvider)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            var modelBindingActionInvokerFactory = serviceProvider.GetService<IModelBindingActionInvokerFactory>();
            if (!(modelBindingActionInvokerFactory is ModelBindingActionInvokerFactory))
            {
                throw new InvalidOperationException($"Route testing requires {nameof(ModelBindingActionInvokerProvider)} and {nameof(IModelBindingActionInvokerFactory)} services to be registered. You may use the '{nameof(ServiceCollectionInternalExtensions.AddModelBindingActionInvoker)}' extension method in the '{TestFramework.TestFrameworkName}.Internal.Services' namespace on your {nameof(IServiceCollection)}.");
            }

            var routingServicesType = typeof(IRoutingServices);

            var routingServices = serviceProvider.GetService<IRoutingServices>();
            if (routingServices == null)
            {
                foreach (var serviceDescriptor in serviceCollection)
                {
                    if (serviceDescriptor.ServiceType == routingServicesType)
                    {
                        routingServices = serviceDescriptor.ImplementationInstance as IRoutingServices;
                    }
                }
            }

            if (routingServices == null)
            {
                routingServices = serviceCollection.AddRoutingServices();
            }

            routingServices.ServiceProvider = serviceProvider;

            return serviceCollection;
        }
    }
}
