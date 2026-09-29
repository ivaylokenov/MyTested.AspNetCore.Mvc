namespace MyTested.AspNetCore.Mvc
{
    using Internal.Contracts;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains controller extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionControllersExtensions
    {
        /// <summary>
        /// Adds <see cref="IActionContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
#pragma warning disable ASPDEPR006 // Obsolete ActionContextAccessor, still registered for applications which use it.
            return serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
#pragma warning restore ASPDEPR006 // Obsolete ActionContextAccessor, still registered for applications which use it.
        }

        /// <summary>
        /// Adds controller testing services.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddControllersCoreTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            
            serviceCollection.TryAddSingleton<IValidControllersCache, ValidControllersCache>();

            serviceCollection.Configure<MvcOptions>(options => options.Conventions.Add(new ValidControllersCache()));

            return serviceCollection;
        }
    }
}
