namespace MyTested.Mvc
{
    using Internal.Caching;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains caching extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionCachingExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="IMemoryCache"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceMemoryCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.Replace<IMemoryCache, MockedMemoryCache>(ServiceLifetime.Transient);
        }
    }
}
