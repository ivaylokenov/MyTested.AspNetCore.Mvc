namespace MyTested.Mvc
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class TestPlugin : IDefaultRegistrationPlugin
    {
        public int Priority => int.MinValue;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection.AddMvc();
    }
}
