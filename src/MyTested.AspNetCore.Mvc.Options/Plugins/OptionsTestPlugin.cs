namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class OptionsTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultOptionsServiceType = typeof(IOptions<>);
        private readonly Type defaultOptionsImplementationType = typeof(OptionsManager<>);
        
        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
                serviceDescriptor.ServiceType == this.defaultOptionsServiceType &&
                serviceDescriptor.ImplementationType == this.defaultOptionsImplementationType;

        public Action<IServiceCollection> ServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.ReplaceOptions();
    }
}
