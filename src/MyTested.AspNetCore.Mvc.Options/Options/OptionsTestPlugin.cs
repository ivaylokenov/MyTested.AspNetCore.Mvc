namespace MyTested.AspNetCore.Mvc.Options
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    public class OptionsTestPlugin : IServiceRegistrationPlugin
    {
        private const string OptionsPrefix = "IOptions<";

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
        {
            get
            {
                return
                    serviceDescriptor =>
                        serviceDescriptor.ServiceType.Name.StartsWith(OptionsPrefix);
            }
        }

        public Action<IServiceCollection> ServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .ToArray()
                .ForEach(s => serviceCollection.ReplaceLifetime(s.ServiceType, ServiceLifetime.Scoped));
    }
}
