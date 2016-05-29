namespace MyTested.Mvc
{
    using Internal.Http;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;

    public static class SessionServiceCollectionExtensions
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
