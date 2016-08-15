namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection
                .AddMvcCoreTesting()
                .AddStringInputFormatter()
                .ReplaceTempDataProvider()
                .ReplaceDbContext()
                .ReplaceMemoryCache()
                .ReplaceOptions()
                .ReplaceSession();

            return serviceCollection;
        }
    }
}
