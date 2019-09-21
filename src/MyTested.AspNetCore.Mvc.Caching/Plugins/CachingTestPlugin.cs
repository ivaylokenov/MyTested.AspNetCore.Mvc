namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using System.Linq;
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

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
            {
                var isSupportedService = 
                    serviceDescriptor.ServiceType == this.defaultCachingServiceType ||
                    serviceDescriptor.ServiceType == this.defaultDistributedCachingServiceType;

                var isSupportedImplementation =
                    serviceDescriptor.ImplementationType == this.defaultCachingImplementationType ||
                    serviceDescriptor.ImplementationType == this.defaultDistributedCachingImplementationType;

                return isSupportedService && isSupportedImplementation;
            };
                
        public Action<IServiceCollection> ServiceRegistrationDelegate
        {
            get
            {
                return serviceCollection =>
                {
                    CommonValidator.CheckForNullReference(serviceCollection);

                    if (this.ShouldReplace(serviceCollection, this.defaultCachingServiceType, this.defaultCachingImplementationType))
                    {
                        serviceCollection.ReplaceMemoryCache();
                        TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IMemoryCache>()?.Dispose();
                    }

                    if (this.ShouldReplace(serviceCollection, this.defaultDistributedCachingServiceType, this.defaultDistributedCachingImplementationType))
                    {
                        serviceCollection.ReplaceDistributedCache();
                        TestHelper.GlobalTestCleanup += () => TestServiceProvider.GetService<IDistributedCacheMock>()?.Dispose();
                    }
                };
            }
        }

        private bool ShouldReplace(IServiceCollection serviceCollection, Type serviceType, Type implementationType)
        {
            var existingService = serviceCollection
                .FirstOrDefault(s => s.ServiceType == serviceType);

            return existingService == null || 
                   existingService.ImplementationType == implementationType;
        }
            
    }
}
