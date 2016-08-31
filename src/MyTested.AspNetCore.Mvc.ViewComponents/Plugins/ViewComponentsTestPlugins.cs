namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class ViewComponentsTestPlugins : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin
    {
        public long Priority => -1000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddViews()
                .AddDataAnnotations()
                .AddJsonFormatters();
        
        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddViewComponentsTesting();
    }
}
