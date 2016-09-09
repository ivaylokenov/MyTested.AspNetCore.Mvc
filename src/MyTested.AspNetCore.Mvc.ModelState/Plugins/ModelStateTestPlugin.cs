namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class ModelStateTestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => -3000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddDataAnnotations();
    }
}
