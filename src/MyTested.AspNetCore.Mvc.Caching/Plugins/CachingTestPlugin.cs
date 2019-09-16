namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.Services;
    using Microsoft.Extensions.Caching.Distributed;

    public class CachingTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultCachingServiceType = typeof(IMemoryCache);
        private readonly Type defaultCachingImplementationType = typeof(MemoryCache);

        private readonly Type defaultDistributedCachingServiceType = typeof(IDistributedCache);
        private readonly Type defaultDistributedCachingImplementationType = typeof(MemoryDistributedCache);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
            {
                var isValidServiceType = 
                    serviceDescriptor.ServiceType == this.defaultCachingServiceType ||
                    serviceDescriptor.ServiceType == this.defaultDistributedCachingServiceType;

                var isValidImplementationType =
                    serviceDescriptor.ImplementationType == this.defaultCachingImplementationType ||
                    serviceDescriptor.ImplementationType == this.defaultDistributedCachingImplementationType;


                return isValidServiceType && isValidImplementationType;
            };
                
        public Action<IServiceCollection> ServiceRegistrationDelegate
        {
            get
            {
                return serviceCollection =>
                {
                    serviceCollection.ReplaceMemoryCache();
                    serviceCollection.ReplaceDistributedCache();

                    TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                };
            }
        }
    }
}
