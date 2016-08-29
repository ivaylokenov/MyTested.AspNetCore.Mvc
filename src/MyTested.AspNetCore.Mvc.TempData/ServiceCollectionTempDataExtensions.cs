namespace MyTested.AspNetCore.Mvc
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains view features extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionTempDataExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="ITempDataProvider"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceTempDataProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection.ReplaceSingleton<ITempDataProvider, TempDataProviderMock>();
        }
    }
}
