namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal;
    using Internal.Application;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    public class CachingTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultCachingServiceType = typeof(IMemoryCache);
        private readonly Type defaultCachingImplementationType = typeof(MemoryCache);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
        {
            get
            {
                return
                    serviceDescriptor =>
                        serviceDescriptor.ServiceType == defaultCachingServiceType &&
                        serviceDescriptor.ImplementationType == defaultCachingImplementationType;
            }
        }

        public Action<IServiceCollection> ServiceRegistrationDelegate
        {
            get
            {
                return serviceCollection =>
                {
                    serviceCollection.ReplaceMemoryCache();
                    TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                };
            }
        }
    }
}
