namespace MyTested.Mvc.Internal.Contracts
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Abstractions;
    using Microsoft.AspNet.Mvc.Controllers;

    public interface IModelBindingActionInvokerFactory
    {
        IActionInvoker CreateModelBindingActionInvoker(
            ActionContext actionContext,
            ControllerActionDescriptor controllerActionDescriptor);
    }
}
