namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;

    public class TestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => 0;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection.AddMvc();
    }
}
