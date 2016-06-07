namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Internal.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;

    public class CustomModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        public IActionInvoker CreateModelBindingActionInvoker(ActionContext actionContext, ControllerActionDescriptor controllerActionDescriptor)
        {
            return null;
        }
    }
}
