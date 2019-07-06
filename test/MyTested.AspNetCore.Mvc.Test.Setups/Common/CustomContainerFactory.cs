namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class CustomContainerFactory : IServiceProviderFactory<CustomContainer>
    {
        public CustomContainer CreateBuilder(IServiceCollection services)
        {
            var container = new CustomContainer();
            container.Populate(new ServiceCollection { services });
            return container;
        }

        public IServiceProvider CreateServiceProvider(CustomContainer containerBuilder)
        {
            containerBuilder.Build();
            return containerBuilder;
        }
    }
}
