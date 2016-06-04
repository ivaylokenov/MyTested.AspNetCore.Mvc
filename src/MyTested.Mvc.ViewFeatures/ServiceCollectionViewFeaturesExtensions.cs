namespace MyTested.Mvc
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains view features extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionViewFeaturesExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="ITempDataProvider"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceTempDataProvider(this IServiceCollection serviceCollection)
        {
            serviceCollection.ReplaceSingleton<ITempDataProvider, MockedTempDataProvider>();
        }
    }
}
