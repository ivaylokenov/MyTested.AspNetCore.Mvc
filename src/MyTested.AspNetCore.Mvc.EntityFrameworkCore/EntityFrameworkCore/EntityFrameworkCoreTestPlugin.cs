namespace MyTested.AspNetCore.Mvc.EntityFrameworkCore
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class EntityFrameworkCoreTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type baseDbContextType = typeof(DbContext);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
        {
            get
            {
                return
                    serviceDescriptor =>
                        baseDbContextType.IsAssignableFrom(serviceDescriptor.ImplementationType);
            }
        }

        public Action<IServiceCollection> ServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceDbContext();
    }
}
