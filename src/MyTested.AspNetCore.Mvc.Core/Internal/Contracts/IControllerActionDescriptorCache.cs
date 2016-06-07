namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.Controllers;

    /// <summary>
    /// Caches controller action descriptors by MethodInfo.
    /// </summary>
    public interface IControllerActionDescriptorCache
    {
        /// <summary>
        /// Gets the controller action descriptor for the provided method info.
        /// </summary>
        /// <param name="methodInfo">Method info of the controller action descriptor to get.</param>
        /// <returns>Controller action descriptor.</returns>
        ControllerActionDescriptor GetActionDescriptor(MethodInfo methodInfo);

        ControllerActionDescriptor TryGetActionDescriptor(MethodInfo methodInfo);
    }
}
