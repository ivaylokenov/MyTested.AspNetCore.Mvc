namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using System.Reflection;

    /// <summary>
    /// Caches view component descriptors by <see cref="MethodInfo"/>.
    /// </summary>
    public interface IViewComponentDescriptorCache
    {
        /// <summary>
        /// Gets the view component descriptor for the provided method info.
        /// </summary>
        /// <param name="methodInfo">Method info of the view component descriptor to get.</param>
        /// <returns>View component descriptor.</returns>
        ViewComponentDescriptor GetViewComponentDescriptor(MethodInfo methodInfo);

        ViewComponentDescriptor TryGetViewComponentDescriptor(MethodInfo methodInfo);
    }
}
