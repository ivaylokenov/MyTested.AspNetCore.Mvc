namespace MyTested.AspNetCore.Mvc
{
    using System.Linq;
    using Internal.Contracts;
    using Internal.Routing;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Validators;

    public static class ServiceCollectionRoutingExtensions
    {
        public static IServiceCollection AddRoutingTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            var modelBindingActionInvokerFactoryServiceType = typeof(IModelBindingActionInvokerFactory);

            if (serviceCollection.All(s => s.ServiceType != modelBindingActionInvokerFactoryServiceType))
            {
                serviceCollection.TryAddEnumerable(
                    ServiceDescriptor.Transient<IActionInvokerProvider, ModelBindingActionInvokerProvider>());
                serviceCollection.TryAddSingleton(modelBindingActionInvokerFactoryServiceType, typeof(ModelBindingActionInvokerFactory));
            }

            return serviceCollection;
        }
    }
}
