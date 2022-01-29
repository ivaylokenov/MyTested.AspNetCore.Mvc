namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class OptionsTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultOptionsServiceType = typeof(IOptions<>);
        private readonly string defaultOptionsImplementationTypeName = "Microsoft.Extensions.Options.UnnamedOptionsManager`1";
        
        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
                serviceDescriptor.ServiceType == this.defaultOptionsServiceType &&
                serviceDescriptor.ImplementationType.FullName == this.defaultOptionsImplementationTypeName;

        public Action<IServiceCollection> ServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.ReplaceOptions();
    }
}
