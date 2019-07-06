namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class CustomContainer : IServiceProvider
    {
        private IServiceProvider inner;
        
        public IServiceCollection Services { get; private set; }

        public string Environment { get; set; }

        public object GetService(Type serviceType) => this.inner.GetService(serviceType);

        public void Populate(IServiceCollection services) => this.Services = services;

        public void Build() => this.inner = this.Services.BuildServiceProvider();
    }
}
