namespace MyTested.Mvc.Internal.Routes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.AspNet.Mvc.Filters;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.AspNet.Mvc.ModelBinding;
    using Microsoft.AspNet.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.Logging;

    public class ModelBindingActionInvoker : ControllerActionInvoker, IModelBindingActionInvoker
    {
        private readonly ControllerActionDescriptor controllerActionDescriptor;

        public ModelBindingActionInvoker(
            ActionContext actionContext,
            IReadOnlyList<IFilterProvider> filterProviders,
            IControllerFactory controllerFactory,
            ControllerActionDescriptor descriptor,
            IReadOnlyList<IInputFormatter> inputFormatters,
            IControllerActionArgumentBinder controllerActionArgumentBinder,
            IReadOnlyList<IModelBinder> modelBinders,
            IReadOnlyList<IModelValidatorProvider> modelValidatorProviders,
            IReadOnlyList<IValueProviderFactory> valueProviderFactories,
            ILogger logger,
            DiagnosticSource diagnosticSource,
            int maxModelValidationErrors)
                : base(actionContext, filterProviders, controllerFactory, descriptor, inputFormatters, controllerActionArgumentBinder, modelBinders, modelValidatorProviders, valueProviderFactories, logger, diagnosticSource, maxModelValidationErrors)
        {
            this.controllerActionDescriptor = descriptor;
        }

        public IDictionary<string, object> BoundActionArguments { get; private set; }

        protected override async Task<IDictionary<string, object>> BindActionArgumentsAsync()
        {
            return (this.BoundActionArguments = await base.BindActionArgumentsAsync());
        }

        protected override Task<IActionResult> InvokeActionAsync(ActionExecutingContext actionExecutingContext)
        {
            return Task.FromResult<IActionResult>(new EmptyResult());
        }
    }
}
