namespace MyTested.AspNetCore.Mvc.ViewFeatures
{
    using System;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    public class ViewFeaturesTestPlugin : IDefaultRegistrationPlugin, IServiceRegistrationPlugin
    {
        private readonly Type defaultTempDataServiceType = typeof(ITempDataProvider);
        private readonly Type defaultTempDataImplementationType = typeof(SessionStateTempDataProvider);

        public long Priority => -1000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddViews()
                .AddDataAnnotations()
                .AddJsonFormatters();

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
        {
            get
            {
                return
                    serviceDescriptor =>
                        serviceDescriptor.ServiceType == defaultTempDataServiceType &&
                        serviceDescriptor.ImplementationType == defaultTempDataImplementationType;
            }
        }

        public Action<IServiceCollection> ServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceTempDataProvider();
    }
}
