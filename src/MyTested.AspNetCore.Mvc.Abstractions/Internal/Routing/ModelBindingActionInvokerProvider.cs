namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;

    public class ModelBindingActionInvokerProvider : IActionInvokerProvider
    {
        private readonly IModelBindingActionInvokerFactory actionInvokerFactory;

        public ModelBindingActionInvokerProvider(IModelBindingActionInvokerFactory actionInvokerFactory) 
            => this.actionInvokerFactory = actionInvokerFactory;

        public int Order => int.MaxValue;
        
        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.ActionContext.ActionDescriptor is ControllerActionDescriptor)
            {
                context.Result = this.actionInvokerFactory.CreateModelBindingActionInvoker(context.ActionContext);
            }
        }
        
        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
            // Intentionally does nothing.
        }
    }
}
