namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class ViewActionResultsTestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => -1000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddViews()
                .AddDataAnnotations();
    }
}
