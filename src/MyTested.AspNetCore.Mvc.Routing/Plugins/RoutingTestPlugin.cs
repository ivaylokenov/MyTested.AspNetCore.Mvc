namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class RoutingTestPlugin : IDefaultRegistrationPlugin, IRoutingServiceRegistrationPlugin
    {
        public long Priority => -8000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate 
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddJsonFormatters();

        public Action<IServiceCollection> RoutingServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.AddRoutingTesting();
    }
}
