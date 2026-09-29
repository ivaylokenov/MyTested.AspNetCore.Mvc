namespace MyTested.AspNetCore.Mvc.Internal.Versioning
{
    using System;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class ApiVersionAwareActionSelectorServiceDescriptor : ServiceDescriptor
    {
        public ApiVersionAwareActionSelectorServiceDescriptor(ServiceDescriptor actionSelectorServiceDescriptor)
            : base(
                typeof(IActionSelector),
                serviceProvider => CreateActionSelector(serviceProvider, actionSelectorServiceDescriptor),
                actionSelectorServiceDescriptor.Lifetime)
            => this.ActionSelectorServiceDescriptor = actionSelectorServiceDescriptor;

        public ServiceDescriptor ActionSelectorServiceDescriptor { get; }

        private static IActionSelector CreateActionSelector(
            IServiceProvider serviceProvider,
            ServiceDescriptor actionSelectorServiceDescriptor)
        {
            object actionSelector;

            if (actionSelectorServiceDescriptor.ImplementationInstance != null)
            {
                actionSelector = actionSelectorServiceDescriptor.ImplementationInstance;
            }
            else if (actionSelectorServiceDescriptor.ImplementationFactory != null)
            {
                actionSelector = actionSelectorServiceDescriptor.ImplementationFactory(serviceProvider);
            }
            else
            {
                actionSelector = ActivatorUtilities.GetServiceOrCreateInstance(
                    serviceProvider,
                    actionSelectorServiceDescriptor.ImplementationType);
            }

            return new ApiVersionAwareActionSelector(
                (IActionSelector)actionSelector,
                serviceProvider.GetRequiredService<IOptions<ApiVersioningOptions>>());
        }
    }
}
