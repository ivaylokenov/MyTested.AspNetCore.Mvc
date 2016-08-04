namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    public static class ServiceCollectionCoreExtensions
    {
        public static IServiceCollection AddMvcCoreTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection
                .AddCoreTesting()
                .AddControllersTesting();

            return serviceCollection;
        }
    }
}
