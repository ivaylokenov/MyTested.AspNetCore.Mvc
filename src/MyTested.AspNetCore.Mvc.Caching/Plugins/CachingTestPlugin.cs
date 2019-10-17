namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal;
    using Internal.Contracts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.Services;
    using Microsoft.Extensions.Caching.Distributed;

    public class CachingTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultMemoryCacheServiceType = typeof(IMemoryCache);
        private readonly Type defaultMemoryCacheImplementationType = typeof(MemoryCache);

        private readonly Type defaultDistributedCacheServiceType = typeof(IDistributedCache);
        private readonly Type defaultDistributedCacheImplementationType = typeof(MemoryDistributedCache);

        private bool replaceMemoryCache = false;
        private bool replaceDistributedCache = false;

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
            {
                this.replaceMemoryCache = this.replaceMemoryCache ||
                    (serviceDescriptor.ServiceType == this.defaultMemoryCacheServiceType &&
                    serviceDescriptor.ImplementationType == this.defaultMemoryCacheImplementationType);

                this.replaceDistributedCache = this.replaceDistributedCache ||
                    (serviceDescriptor.ServiceType == this.defaultDistributedCacheServiceType &&
                    serviceDescriptor.ImplementationType == this.defaultDistributedCacheImplementationType);

                return replaceMemoryCache || replaceDistributedCache;
            };
                
        public Action<IServiceCollection> ServiceRegistrationDelegate
            => serviceCollection =>
            {
                if (this.replaceMemoryCache)
                {
                    serviceCollection.ReplaceMemoryCache();
                    TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                }

                if (this.replaceDistributedCache)
                {
                    serviceCollection.ReplaceDistributedCache();
                    TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IDistributedCacheMock>()?.Dispose();
                }
            };
    }
}
