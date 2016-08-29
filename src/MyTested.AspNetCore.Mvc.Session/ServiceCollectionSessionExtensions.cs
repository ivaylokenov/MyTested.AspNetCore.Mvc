namespace MyTested.AspNetCore.Mvc
{
    using Internal.Session;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains session extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionSessionExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="ISessionStore"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceSession(this IServiceCollection serviceCollection)
        {
            return serviceCollection.ReplaceTransient<ISessionStore, SessionStoreMock>();
        }
    }
}
