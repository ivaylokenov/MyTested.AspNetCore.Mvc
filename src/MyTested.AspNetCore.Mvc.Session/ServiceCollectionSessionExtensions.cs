namespace MyTested.AspNetCore.Mvc
{
    using Internal.Http;
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
        public static void ReplaceSession(this IServiceCollection serviceCollection)
        {
            serviceCollection.ReplaceTransient<ISessionStore, MockedSessionStore>();
        }
    }
}
