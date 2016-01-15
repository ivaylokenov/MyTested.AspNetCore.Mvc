namespace MyTested.Mvc.Internal.Contracts
{
    using System.Reflection;
    using Microsoft.AspNet.Mvc.Controllers;

    public interface IControllerActionDescriptorCache
    {
        ControllerActionDescriptor GetActionDescriptor(MethodInfo methodInfo);
    }
}
