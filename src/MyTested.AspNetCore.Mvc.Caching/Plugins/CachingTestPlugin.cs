namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal;
    using Internal.Contracts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.Services;
    using Microsoft.Extensions.Caching.Distributed;
    using Utilities.Validators;

    public class CachingTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultCachingServiceType = typeof(IMemoryCache);
        private readonly Type defaultCachingImplementationType = typeof(MemoryCache);

        private readonly Type defaultDistributedCachingServiceType = typeof(IDistributedCache);
        private readonly Type defaultDistributedCachingImplementationType = typeof(MemoryDistributedCache);

        private bool shouldReplaceMemoryCache = false;
        private bool shouldReplaceDistributedCache = false;

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
            {
                var isValidServiceType = 
                    serviceDescriptor.ServiceType == this.defaultCachingServiceType ||
                    serviceDescriptor.ServiceType == this.defaultDistributedCachingServiceType;

                var isValidImplementationType =
                    serviceDescriptor.ImplementationType == this.defaultCachingImplementationType ||
                    serviceDescriptor.ImplementationType == this.defaultDistributedCachingImplementationType;

                if (serviceDescriptor.ServiceType == this.defaultCachingServiceType)
                {
                    this.shouldReplaceMemoryCache = serviceDescriptor.ImplementationType == defaultCachingImplementationType;
                }

                if (serviceDescriptor.ServiceType == this.defaultDistributedCachingServiceType)
                {
                    this.shouldReplaceDistributedCache = serviceDescriptor.ImplementationType == defaultDistributedCachingImplementationType;
                }
                        

                return isValidServiceType && isValidImplementationType;
            };
                
        public Action<IServiceCollection> ServiceRegistrationDelegate
        {
            get
            {
                return serviceCollection =>
                {
                    CommonValidator.CheckForNullReference(serviceCollection);

                    if (this.shouldReplaceMemoryCache)
                    {
                        serviceCollection.ReplaceMemoryCache();
                        TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                    }

                    if (this.shouldReplaceDistributedCache)
                    {
                        serviceCollection.ReplaceDistributedCache();
                        TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IDistributedCacheMock>()?.Dispose();
                    }
                };
            }
        }
    }
}
