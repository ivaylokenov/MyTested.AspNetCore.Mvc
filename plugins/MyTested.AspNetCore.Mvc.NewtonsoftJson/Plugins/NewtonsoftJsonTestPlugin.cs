namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class NewtonsoftJsonTestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => -500;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddNewtonsoftJson();
    }
}
