namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionUniverseExtensions
    {
        /// <summary>
        /// Adds all testing services available in the testing framework.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddMvcUniverseTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection
                .AddMvcTesting()
                .ReplaceDbContext()
                .ReplaceMemoryCache()
                .ReplaceSession();

            return serviceCollection;
        }
    }
}
