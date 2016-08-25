namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    public static class UniverseServiceCollectionExtensions
    {
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
