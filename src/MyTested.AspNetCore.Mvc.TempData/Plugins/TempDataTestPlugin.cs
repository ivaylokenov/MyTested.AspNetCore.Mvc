namespace MyTested.AspNetCore.Mvc.Plugins
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class TempDataTestPlugin : IDefaultRegistrationPlugin, IServiceRegistrationPlugin
    {
        public long Priority => -1000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddViews()
                .AddDataAnnotations();

        private readonly Type defaultTempDataServiceType = typeof(ITempDataProvider);
        private readonly Type defaultTempDataImplementationType = typeof(CookieTempDataProvider);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
                serviceDescriptor.ServiceType == this.defaultTempDataServiceType &&
                serviceDescriptor.ImplementationType == this.defaultTempDataImplementationType;

        public Action<IServiceCollection> ServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.ReplaceTempDataProvider();
    }
}
