namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Internal.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;

    public class CustomModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        public IActionInvoker CreateModelBindingActionInvoker(ActionContext actionContext)
        {
            return null;
        }
    }
}
