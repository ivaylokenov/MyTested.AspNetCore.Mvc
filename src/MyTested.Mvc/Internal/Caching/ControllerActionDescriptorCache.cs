namespace MyTested.Mvc.Internal.Caching
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.AspNet.Mvc.Infrastructure;

    /// <summary>
    /// Caches controller action descriptors by MethodInfo.
    /// </summary>
    public class ControllerActionDescriptorCache : IControllerActionDescriptorCache
    {
        private static readonly ConcurrentDictionary<MethodInfo, ControllerActionDescriptor> Cache =
            new ConcurrentDictionary<MethodInfo, ControllerActionDescriptor>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerActionDescriptorCache" /> class.
        /// </summary>
        /// <param name="provider">Action descriptors collection provider.</param>
        public ControllerActionDescriptorCache(IActionDescriptorsCollectionProvider provider)
        {
            this.PrepareCache(provider);
        }

        /// <summary>
        /// Gets the controller action descriptor for the provided method info.
        /// </summary>
        /// <param name="methodInfo">Method info of the controller action descriptor to get.</param>
        /// <returns>Controller action descriptor.</returns>
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
