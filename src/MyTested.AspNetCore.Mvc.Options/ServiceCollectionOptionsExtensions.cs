namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Contains options extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionOptionsExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="IOptions{TOptions}"/> with a scoped implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceOptions(this IServiceCollection serviceCollection) 
            => serviceCollection.Replace(typeof(IOptions<>), typeof(OptionsManager<>), ServiceLifetime.Scoped);
    }
}
