namespace MyTested.Mvc.Internal.Caching
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.AspNet.Mvc.Infrastructure;

    public class ControllerActionDescriptorCache : IControllerActionDescriptorCache
    {
        private static readonly ConcurrentDictionary<MethodInfo, ControllerActionDescriptor> Cache =
            new ConcurrentDictionary<MethodInfo, ControllerActionDescriptor>();
        
        public ControllerActionDescriptorCache(IActionDescriptorsCollectionProvider provider)
        {
            this.PrepareCache(provider);
        }

        public ControllerActionDescriptor GetActionDescriptor(MethodInfo methodInfo)
        {
            ControllerActionDescriptor result = null;
            Cache.TryGetValue(methodInfo, out result);

            return result;
        }

        private void PrepareCache(IActionDescriptorsCollectionProvider provider)
        {
            var controllerActionDescriptors = provider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>();

            foreach (var descriptor in controllerActionDescriptors)
            {
                Cache.TryAdd(descriptor.MethodInfo, descriptor);
            }
        }
    }
}
