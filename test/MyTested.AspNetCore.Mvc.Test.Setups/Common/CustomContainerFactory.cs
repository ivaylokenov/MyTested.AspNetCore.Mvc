namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class CustomContainerFactory : IServiceProviderFactory<CustomContainer>
    {
        public CustomContainer CreateBuilder(IServiceCollection services)
        {
            var container = new CustomContainer();
            container.Populate(services);
            return container;
        }

        public IServiceProvider CreateServiceProvider(CustomContainer containerBuilder)
        {
            containerBuilder.Build();
            return containerBuilder;
        }
    }
}
