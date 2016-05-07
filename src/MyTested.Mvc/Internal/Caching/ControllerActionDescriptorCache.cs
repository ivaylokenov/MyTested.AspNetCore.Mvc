namespace MyTested.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    /// <summary>
    /// Caches controller action descriptors by MethodInfo.
    /// </summary>
    public class ControllerActionDescriptorCache : IControllerActionDescriptorCache
    {
        private static readonly ConcurrentDictionary<MethodInfo, ControllerActionDescriptor> Cache =
            new ConcurrentDictionary<MethodInfo, ControllerActionDescriptor>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerActionDescriptorCache"/> class.
        /// </summary>
        /// <param name="provider">Action descriptors collection provider.</param>
        public ControllerActionDescriptorCache(IActionDescriptorCollectionProvider provider)
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
            if (!Cache.Any())
            {
                throw new InvalidOperationException("Controller actions could not be found by the MVC application. Controllers may need to be added as services by calling 'AddMvc().AddControllersAsServices()' or 'AddMvcControllersAsServices()' on the service collection.");
            }

            return this.TryGetActionDescriptor(methodInfo);
        }

        public ControllerActionDescriptor TryGetActionDescriptor(MethodInfo methodInfo)
        {
            ControllerActionDescriptor controllerActionDescriptor = null;
            Cache.TryGetValue(methodInfo, out controllerActionDescriptor);

            return controllerActionDescriptor;
        }

        private void PrepareCache(IActionDescriptorCollectionProvider provider)
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
