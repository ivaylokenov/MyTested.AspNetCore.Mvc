namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Utilities.Extensions;

    /// <summary>
    /// Caches view component descriptors by <see cref="MethodInfo"/>.
    /// </summary>
    public class ViewComponentDescriptorCache : IViewComponentDescriptorCache
    {
        private static readonly ConcurrentDictionary<MethodInfo, ViewComponentDescriptor> Cache =
            new ConcurrentDictionary<MethodInfo, ViewComponentDescriptor>();
        
        public ViewComponentDescriptorCache(IViewComponentDescriptorCollectionProvider provider) 
            => this.PrepareCache(provider);

        /// <inheritdoc />
        public ViewComponentDescriptor GetViewComponentDescriptor(MethodInfo methodInfo)
        {
            if (!Cache.Any())
            {
                throw new InvalidOperationException("View components could not be found by the MVC application. View components may need to be added as services by calling 'AddControllers().AddViewComponentsAsServices()' or 'AddControllersWithViews().AddViewComponentsAsServices()' on the service collection. You may also add the external view components assembly as an application part by calling 'AddControllers().AddApplicationPart()' or 'AddControllersWithViews().AddApplicationPart()'.");
            }

            return this.TryGetViewComponentDescriptor(methodInfo);
        }

        /// <inheritdoc />
        public ViewComponentDescriptor TryGetViewComponentDescriptor(MethodInfo methodInfo)
        {
            Cache.TryGetValue(methodInfo, out var viewComponentDescriptor);

            return viewComponentDescriptor;
        }

        private void PrepareCache(IViewComponentDescriptorCollectionProvider provider)
            => provider
                .ViewComponents
                .Items
                .ForEach(descriptor => Cache.TryAdd(descriptor.MethodInfo, descriptor));
    }
}
