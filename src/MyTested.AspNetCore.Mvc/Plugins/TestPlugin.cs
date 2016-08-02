namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class TestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => 0;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection.AddMvc();
    }
}
