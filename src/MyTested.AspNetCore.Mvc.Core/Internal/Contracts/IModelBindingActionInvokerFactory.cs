namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;

    public interface IModelBindingActionInvokerFactory
    {
        IActionInvoker CreateModelBindingActionInvoker(
            ActionContext actionContext,
            ControllerActionDescriptor controllerActionDescriptor);
    }
}
