namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class CustomServiceProvider : IServiceProvider
    {
        private readonly IServiceCollection services;

        private IServiceProvider serviceProvider;

        public CustomServiceProvider() 
            => this.services = new ServiceCollection();

        public void Add(ServiceDescriptor serviceDescriptor) 
            => this.services.Add(serviceDescriptor);

        public void Build()
            => this.serviceProvider = this.services.BuildServiceProvider();

        public object GetService(Type serviceType)
            => this.serviceProvider.GetService(serviceType);
    }
}
