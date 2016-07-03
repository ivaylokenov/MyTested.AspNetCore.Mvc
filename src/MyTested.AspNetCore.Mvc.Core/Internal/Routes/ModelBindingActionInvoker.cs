namespace MyTested.AspNetCore.Mvc.Internal.Routes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.Logging;

    public class ModelBindingActionInvoker : ControllerActionInvoker, IModelBindingActionInvoker
    {
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
        }

        public IDictionary<string, object> BoundActionArguments { get; private set; }
        
        public override Task InvokeAsync()
        {
            return TaskCache.CompletedTask;
        }
    }
}
