namespace MyTested.Mvc.Internal.Contracts
{
    using Microsoft.AspNet.Mvc.Controllers;
    using System.Reflection;

    public interface IControllerActionDescriptorCache
    {
        ControllerActionDescriptor GetActionDescriptor(MethodInfo methodInfo);
    }
}
