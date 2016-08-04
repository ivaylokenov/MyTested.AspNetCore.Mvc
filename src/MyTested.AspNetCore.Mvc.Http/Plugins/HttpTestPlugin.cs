namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class HttpTestPlugin : BaseTestPlugin, IServiceRegistrationPlugin
    {
        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddStringInputFormatter();
    }
}
