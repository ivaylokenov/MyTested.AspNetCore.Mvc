namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;

    public interface IModelBindingActionInvokerFactory
    {
        IActionInvoker CreateModelBindingActionInvoker(ActionContext actionContext);
    }
}
