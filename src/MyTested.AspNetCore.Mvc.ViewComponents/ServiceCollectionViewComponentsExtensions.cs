namespace MyTested.AspNetCore.Mvc
{
    using Internal.Caching;
    using Internal.Contracts;
    using Internal.ViewComponents;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains view component extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionViewComponentsExtensions
    {
        /// <summary>
        /// Adds view component testing services.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddViewComponentsTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection.TryAddSingleton<IViewComponentPropertyActivator, ViewComponentPropertyActivator>();
            serviceCollection.TryAddSingleton<IViewComponentDescriptorCache, ViewComponentDescriptorCache>();

            return serviceCollection;
        }
    }
}
