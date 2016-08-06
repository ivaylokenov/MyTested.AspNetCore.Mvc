namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class HttpTestPlugin : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin
    {
        public long Priority => -9000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate =>
            serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddJsonFormatters();

        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddStringInputFormatter();
    }
}
