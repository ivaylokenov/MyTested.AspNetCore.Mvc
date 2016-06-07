namespace MyTested.Mvc.DataAnnotations.DataAnnotations
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class DataAnnotationsTestPlugin : IDefaultRegistrationPlugin
    {
        public long Priority => -100;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection.AddMvcCore().AddDataAnnotations();
    }
}
