namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;

    public class ModelBindingActionInvoker : ControllerActionInvoker, IModelBindingActionInvoker
    {
        private readonly IControllerFactory controllerFactory;
        private readonly IControllerArgumentBinder controllerArgumentBinder;
        private readonly ControllerContext controllerContext;

        public ModelBindingActionInvoker(
            ControllerActionInvokerCache cache,
            IControllerFactory controllerFactory,
            IControllerArgumentBinder controllerArgumentBinder,
            ILogger logger,
            DiagnosticSource diagnosticSource,
            ActionContext actionContext,
            IReadOnlyList<IValueProviderFactory> valueProviderFactories,
            int maxModelValidationErrors)
                : base(cache, controllerFactory, controllerArgumentBinder, logger, diagnosticSource, actionContext, valueProviderFactories, maxModelValidationErrors)
        {
            this.BoundActionArguments = new Dictionary<string, object>();

            this.controllerFactory = controllerFactory;
            this.controllerArgumentBinder = controllerArgumentBinder;

            this.controllerContext = new ControllerContext(actionContext);
            this.controllerContext.ModelState.MaxAllowedErrors = maxModelValidationErrors;
            this.controllerContext.ValueProviderFactories = new List<IValueProviderFactory>(valueProviderFactories);
        }

        public IDictionary<string, object> BoundActionArguments { get; private set; }
        
        public override Task InvokeAsync()
        {
            var controller = this.controllerFactory.CreateController(this.controllerContext);
            this.controllerArgumentBinder.BindArgumentsAsync(controllerContext, controller, this.BoundActionArguments);

            return TaskCache.CompletedTask;
        }
    }
}
