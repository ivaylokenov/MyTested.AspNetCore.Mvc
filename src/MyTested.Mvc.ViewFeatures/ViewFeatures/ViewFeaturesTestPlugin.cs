namespace MyTested.Mvc.ViewFeatures
{
    using System;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    public class ViewFeaturesTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultTempDataServiceType = typeof(ITempDataProvider);
        private readonly Type defaultTempDataImplementationType = typeof(SessionStateTempDataProvider);

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
