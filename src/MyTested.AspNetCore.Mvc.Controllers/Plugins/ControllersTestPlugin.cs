namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class ControllersTestPlugin : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin
    {
        public long Priority => -10000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate => 
            serviceCollection => serviceCollection.AddMvcCore();
        
        public Action<IServiceCollection> ServiceRegistrationDelegate => 
            serviceCollection => serviceCollection.AddControllersTesting();
    }
}
