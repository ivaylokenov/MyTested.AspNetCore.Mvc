namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class AbstractionsTestPlugin : BaseTestPlugin, IServiceRegistrationPlugin, IInitializationPlugin
    {
        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddCoreTesting();

        // this call prepares all application conventions and fills the controller action descriptor cache
        public Action<IServiceProvider> InitializationDelegate => serviceProvider => serviceProvider.GetService<IControllerActionDescriptorCache>();
    }
}
