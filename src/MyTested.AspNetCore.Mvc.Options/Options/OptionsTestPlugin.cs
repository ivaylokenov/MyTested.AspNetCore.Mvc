namespace MyTested.AspNetCore.Mvc.Options
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class OptionsTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultOptionsServiceType = typeof(IOptions<>);
        private readonly Type defaultOptionsImplementationType = typeof(OptionsManager<>);
        
        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
        {
            get
            {
                return
                    serviceDescriptor =>
                        serviceDescriptor.ServiceType == defaultOptionsServiceType &&
                        serviceDescriptor.ImplementationType == defaultOptionsImplementationType;
            }
        }

        public Action<IServiceCollection> ServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceOptions();
    }
}
