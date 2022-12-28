namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class OptionsTestPlugin : IServiceRegistrationPlugin
    {
        private const string DefaultOptionsImplementationTypeFullName = "Microsoft.Extensions.Options.UnnamedOptionsManager`1";

        private readonly Type defaultOptionsServiceType = typeof(IOptions<>);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
                serviceDescriptor.ServiceType == this.defaultOptionsServiceType &&
                serviceDescriptor.ImplementationType?.FullName == DefaultOptionsImplementationTypeFullName;

        public Action<IServiceCollection> ServiceRegistrationDelegate
            => serviceCollection => serviceCollection.ReplaceOptions();
    }
}
