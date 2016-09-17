namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class DataAnnotationsTestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => -2000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddDataAnnotations();
    }
}
