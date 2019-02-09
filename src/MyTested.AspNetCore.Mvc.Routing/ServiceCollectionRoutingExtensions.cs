namespace MyTested.AspNetCore.Mvc
{
    using System.Linq;
    using Internal.Contracts;
    using Internal.Routing;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
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

            if (serviceCollection.All(s => s.ServiceType != modelBindingActionInvokerFactoryServiceType))
            {
                serviceCollection.TryAddEnumerable(
                    ServiceDescriptor.Transient<IActionInvokerProvider, ModelBindingActionInvokerProvider>());
                serviceCollection.TryAddSingleton(modelBindingActionInvokerFactoryServiceType, typeof(ModelBindingActionInvokerFactory));
            }

            // Disable end-point routing until it is fully supported by the test framework.
            serviceCollection.Configure<MvcOptions>(options => options.EnableEndpointRouting = false);

            return serviceCollection;
        }
    }
}
