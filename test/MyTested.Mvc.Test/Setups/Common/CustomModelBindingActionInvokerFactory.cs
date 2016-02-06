namespace MyTested.Mvc.Tests.Setups.Common
{
    using Internal.Contracts;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Abstractions;
    using Microsoft.AspNet.Mvc.Controllers;

    public class CustomModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        public IActionInvoker CreateModelBindingActionInvoker(ActionContext actionContext, ControllerActionDescriptor controllerActionDescriptor)
        {
            return null;
        }
    }
}
