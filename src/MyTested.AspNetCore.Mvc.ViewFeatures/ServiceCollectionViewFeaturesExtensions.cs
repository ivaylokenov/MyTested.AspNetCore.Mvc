namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;

    public static class ServiceCollectionViewFeaturesExtensions
    {
        public static IServiceCollection AddViewFeaturesTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection
                .AddViewComponentsTesting()
                .ReplaceTempDataProvider();

            return serviceCollection;
        }
    }
}
