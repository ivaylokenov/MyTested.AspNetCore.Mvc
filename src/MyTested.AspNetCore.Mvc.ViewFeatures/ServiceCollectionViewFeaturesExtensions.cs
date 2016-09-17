namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    /// <summary>
    /// Contains view features extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionViewFeaturesExtensions
    {
        /// <summary>
        /// Adds view features testing services.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddViewFeaturesTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection
                .AddViewComponentsTesting()
                .ReplaceTempDataProvider();

            return serviceCollection;
        }
    }
}
