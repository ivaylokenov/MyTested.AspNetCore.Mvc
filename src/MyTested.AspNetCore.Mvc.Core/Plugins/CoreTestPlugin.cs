namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Internal;

    public class CoreTestPlugin : IDefaultRegistrationPlugin, IServiceRegistrationPlugin, IRouteServiceRegistrationPlugin, IInitializationPlugin
    {
        private readonly Type defaultMvcMarkerServiceType = typeof(MvcMarkerService);
        
        public long Priority => -10000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddJsonFormatters();

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate => 
            serviceDescriptor => serviceDescriptor.ServiceType == defaultMvcMarkerServiceType;

        public Action<IServiceCollection> ServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceMvcCore();

        public Action<IServiceCollection> RouteServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceMvcRouting();

        // this call prepares all application conventions and fills the controller action descriptor cache
        public Action<IServiceProvider> InitializationDelegate => serviceProvider => serviceProvider.GetService<IControllerActionDescriptorCache>();
    }
}
