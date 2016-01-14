namespace MyTested.Mvc.Internal.Caching
{
    using Contracts;
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.AspNet.Mvc.Infrastructure;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ControllerActionDescriptorCache : IControllerActionDescriptorCache
    {
        private static readonly ConcurrentDictionary<MethodInfo, ControllerActionDescriptor> Cache =
            new ConcurrentDictionary<MethodInfo, ControllerActionDescriptor>();

        private IList<ControllerActionDescriptor> items;

        public ControllerActionDescriptorCache(IActionDescriptorsCollectionProvider provider)
        {
            this.items = provider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .ToList();
        }

        public ControllerActionDescriptor GetActionDescriptor(MethodInfo methodInfo)
        {
            return Cache.GetOrAdd(methodInfo, value =>
            {
                ControllerActionDescriptor foundControllerActionDescriptor = null;
                var actionDescriptors = this.items;
                for (int i = 0; i < actionDescriptors.Count; i++)
                {
                    var actionDescriptor = actionDescriptors[i];
                    if (actionDescriptor.MethodInfo == methodInfo)
                    {
                        foundControllerActionDescriptor = actionDescriptor;
                        break;
                    }
                }
                
                return foundControllerActionDescriptor;
            });
        }
    }
}
