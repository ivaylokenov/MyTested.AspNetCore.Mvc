namespace MyTested.AspNetCore.Mvc.DataAnnotations
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;

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
