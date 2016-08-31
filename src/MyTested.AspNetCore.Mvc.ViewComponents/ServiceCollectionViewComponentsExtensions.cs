namespace MyTested.AspNetCore.Mvc
{
    using Internal.Contracts;
    using Internal.ViewComponents;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Validators;

    public static class ServiceCollectionViewComponentsExtensions
    {
        public static IServiceCollection AddViewComponentsTesting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            serviceCollection.TryAddSingleton<IViewComponentPropertyActivator, ViewComponentPropertyActivator>();

            return serviceCollection;
        }
    }
}
