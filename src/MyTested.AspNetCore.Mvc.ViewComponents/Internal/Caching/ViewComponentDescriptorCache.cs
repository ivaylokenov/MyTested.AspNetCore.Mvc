namespace MyTested.AspNetCore.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Caches view component descriptors by <see cref="MethodInfo"/>.
    /// </summary>
    public class ViewComponentDescriptorCache : IViewComponentDescriptorCache
    {
        private static readonly ConcurrentDictionary<MethodInfo, ViewComponentDescriptor> Cache =
            new ConcurrentDictionary<MethodInfo, ViewComponentDescriptor>();
        
        public ViewComponentDescriptorCache(IViewComponentDescriptorCollectionProvider provider)
        {
            this.PrepareCache(provider);
        }

        /// <inheritdoc />
        public ViewComponentDescriptor GetViewComponentDescriptor(MethodInfo methodInfo)
        {
            if (!Cache.Any())
            {
                throw new InvalidOperationException("View components could not be found by the MVC application. View components may need to be added as services by calling 'AddMvc().AddViewComponentsAsServices()' on the service collection. You may also add the external view components assembly as an application part by calling 'AddMvc().AddApplicationPart()'.");
            }

            return this.TryGetViewComponentDescriptor(methodInfo);
        }

        /// <inheritdoc />
        public ViewComponentDescriptor TryGetViewComponentDescriptor(MethodInfo methodInfo)
        {
            ViewComponentDescriptor viewComponentDescriptor = null;
            Cache.TryGetValue(methodInfo, out viewComponentDescriptor);

            return viewComponentDescriptor;
        }

        private void PrepareCache(IViewComponentDescriptorCollectionProvider provider)
        {
            var viewComponentDescriptors = provider.ViewComponents.Items;

            foreach (var descriptor in viewComponentDescriptors)
            {
                Cache.TryAdd(descriptor.MethodInfo, descriptor);
            }
        }
    }
}
